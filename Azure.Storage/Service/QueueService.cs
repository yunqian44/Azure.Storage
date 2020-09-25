using Azure.Storage.Extension;
using Azure.Storage.Queues;
using Azure.Storage.Queues.Models;
using Microsoft.VisualBasic;
using Microsoft.WindowsAzure.Storage.Queue;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Azure.Storage.Service
{
    public class QueueService : IQueueService
    {
        private readonly QueueClient _queueClient;

        public QueueService(QueueClient queueClient)
        {
            _queueClient = queueClient;
        }



        /// <summary>
        /// 添加消息
        /// </summary>
        /// <param name="msg">消息</param>
        /// <returns></returns>
        public async Task AddMessage(string msg)
        {
            // Create the queue
            _queueClient.CreateIfNotExists();

            if (_queueClient.Exists())
            {
 
                // Send a message to the queue
                 await _queueClient.SendMessageAsync(msg.EncryptBase64());
            }
        }

        public async IAsyncEnumerable<string> GetMessages()
        {
            if (_queueClient.Exists())
            {
                // Peek at the next message
                PeekedMessage[] peekedMessage = await _queueClient.PeekMessagesAsync();
                for (int i = 0; i < peekedMessage.Length; i++)
                {
                    //Display the message
                    yield return string.Format($"Peeked message: '{peekedMessage[i].MessageText.DecodeBase64()}'") ;
                }
            }
        }

        /// <summary>
        /// 处理消息
        /// </summary>
        /// <returns></returns>
        public async Task ProcessingMessage()
        {
            // 执行 getmessage(), 队头的消息会变得不可见。
            QueueMessage[] retrievedMessage = await _queueClient.ReceiveMessagesAsync();
            try
            {
                //处理消息


                // 如果在30s内你没有删除这条消息，它会重新出现在队尾。
                // 所以正确处理一条消息的过程是，处理完成后，删除这条消息
                await _queueClient.DeleteMessageAsync(retrievedMessage[0].MessageId, retrievedMessage[0].PopReceipt);
            }
            catch //(消息处理异常)
            { }
        }

        /// <summary>
        /// 更新已排队的消息
        /// </summary>
        /// <returns></returns>
        public async Task UpdateMessage()
        {
            if (_queueClient.Exists())
            {
                // Get the message from the queue
                QueueMessage[] message = await _queueClient.ReceiveMessagesAsync();

                // Update the message contents
                await _queueClient.UpdateMessageAsync(message[0].MessageId,
                        message[0].PopReceipt,
                        "Updated contents".EncryptBase64(),
                        TimeSpan.FromSeconds(60.0)  // Make it invisible for another 60 seconds
                    );
            }
        }
    }
}
