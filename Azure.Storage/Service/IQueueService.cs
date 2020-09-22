using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Azure.Storage.Service
{
    public interface IQueueService
    {
        /// <summary>
        /// 插入Message
        /// </summary>
        /// <param name="msg">msg</param>
        /// <returns></returns>
        Task AddMessage(string msg);

        /// <summary>
        /// 获取消息
        /// </summary>
        /// <returns></returns>
        IAsyncEnumerable<string> GetMessages();

        /// <summary>
        /// 更新消息
        /// </summary>
        /// <returns></returns>
        Task UpdateMessage();

        /// <summary>
        /// 处理消息
        /// </summary>
        /// <returns></returns>
        Task ProcessingMessage();


    }
}
