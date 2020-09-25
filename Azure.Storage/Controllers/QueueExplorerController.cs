using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Azure.Storage.Service;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Azure.Storage.Controllers
{
    [Route("Queue")]
    public class QueueExplorerController : Controller
    {

        private readonly IQueueService _queueService;

        public QueueExplorerController(IQueueService queueSerivce)
        {
            this._queueService = queueSerivce;
        }

        [HttpPost("AddQueue")]
        public async Task<ActionResult> AddQueue()
        {
            string msg = $"我是添加进去的第一个消息";
            await _queueService.AddMessage(msg);
            return Ok();
        }

        [HttpGet("QueryQueue")]
        public  ActionResult QueryQueue()
        {
            return Ok( _queueService.GetMessages());
            
        }

        [HttpPut("UpdateQueue")]
        public async Task<ActionResult> UpdateQueue()
        {
            await _queueService.UpdateMessage();
            return Ok();
        }

        [HttpGet("ProcessingMessage")]
        public async Task<ActionResult> ProcessingQueue()
        {
            await _queueService.ProcessingMessage();
            return Ok();
        }
    }
}
