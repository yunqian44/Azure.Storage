using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;
using Azure.Storage.Models;
using Azure.Storage.Service;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualBasic;
using Microsoft.WindowsAzure.Storage.Table;

namespace Azure.Storage.Controllers
{
    [Route("Table")]
    public class TableExplorerController : Controller
    {
        private readonly ITableService _tableService;

        public TableExplorerController(ITableService tableService)
        {
            this._tableService = tableService;
        }

        [HttpPost("AddUser")]
        public async Task<ActionResult> AddEntity([FromBody]UserInfo user)
        {
            await _tableService.AddEntity(new UserInfo("zhangsan", "610124199012223650") { Email = "135012689@qq.com", TelNum = "13000000000" });
            return Ok();
        }

        [HttpPost("AddBatchUser")]
        public async Task<ActionResult> AddEntities([FromBody]List<UserInfo> users)
        {
            List<UserInfo> userList = new List<UserInfo>();
            userList.Add(new UserInfo("lisi", "610124199012223651") { Email = "1350126740@qq.com", TelNum = "13000000001" });
            userList.Add(new UserInfo("lisi", "610124199012223652") { Email = "1350126741@qq.com", TelNum = "13000000002" });
            userList.Add(new UserInfo("lisi", "610124199012223653") { Email = "1350126742@qq.com", TelNum = "13000000003" });
            await _tableService.BatchAddEntities(userList);
            return Ok();
        }

        [HttpGet("Users")]
        public ActionResult QueryUsers()
        {
            var filter = TableQuery.CombineFilters(TableQuery.GenerateFilterCondition("PartitionKey", QueryComparisons.Equal, "zhangsan"), TableOperators.And, TableQuery.GenerateFilterCondition("RowKey", QueryComparisons.Equal, "610124199012223650"));

            return Ok(_tableService.QueryUsers(filter));
        }

        [HttpPut("UpdateUser")]
        public async Task<ActionResult> UpdateUser([FromBody]UserInfo user)
        {
            await _tableService.UpdateEntity(new UserInfo("zhangsan", "610124199012223650") { Email = "135012689@qq.com", TelNum = "15000000000" });
            return Ok();
        }

        [HttpDelete("DeleteEntity")]
        public async Task<ActionResult> DeleteEntity([FromBody]UserInfo user)
        {
            await _tableService.DeleteEntity(new UserInfo("lisi", "610124199012223651"));
            return Ok();
        }

        [HttpDelete("{tableName}")]
        public async Task<ActionResult> DeleteTable(string tableName)
        {
            await _tableService.DeleteTable(tableName);
            return Ok();
        }
    }
}