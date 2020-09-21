using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Azure.Storage.Models;
using Azure.Storage.Service;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Cosmos.Table;

namespace Azure.Storage.Controllers
{
    [Route("TableV2")]
    public class TableExplorerV2Controller : Controller
    {
        private readonly ITableServiceV2 _tableService;

        public TableExplorerV2Controller(ITableServiceV2 tableService)
        {
            this._tableService = tableService;
        }

        [HttpPost("AddUser")]
        public async Task<ActionResult> AddEntity([FromBody] UserInfoV2 user)
        {
            await _tableService.AddEntity(new UserInfoV2("huge", "610124199012221000") { Email = "huge@qq.com", TelNum = "13000000000" });
            return Ok();
        }

        [HttpPost("AddBatchUser")]
        public async Task<ActionResult> AddEntities([FromBody] List<UserInfoV2> users)
        {
            List<UserInfoV2> userList = new List<UserInfoV2>();
            userList.Add(new UserInfoV2("wangwei", "610124199012221001") { Email = "wangwei@qq.com", TelNum = "13000000001" });
            userList.Add(new UserInfoV2("wangwei", "610124199012221002") { Email = "wangwei@qq.com", TelNum = "13000000002" });
            userList.Add(new UserInfoV2("wangwei", "610124199012221003") { Email = "wangwei@qq.com", TelNum = "13000000003" });
            await _tableService.BatchAddEntities(userList);
            return Ok();
        }


        [HttpGet("Users")]
        public ActionResult QueryUsers()
        {
            var filter = TableQuery.CombineFilters(TableQuery.GenerateFilterCondition("PartitionKey", QueryComparisons.Equal, "wangwei"), TableOperators.And, TableQuery.GenerateFilterCondition("RowKey", QueryComparisons.Equal, "610124199012221001"));

            return Ok(_tableService.QueryUsers(filter));
        }

        [HttpPut("UpdateUser")]
        public async Task<ActionResult> UpdateUser([FromBody] UserInfoV2 user)
        {
            await _tableService.UpdateEntity(new UserInfoV2("huge", "610124199012221000") { Email = "huge@163.com", TelNum = "15000000000" });
            return Ok();
        }

        [HttpDelete("DeleteEntity")]
        public async Task<ActionResult> DeleteEntity([FromBody] UserInfoV2 user)
        {
            await _tableService.DeleteEntity(new UserInfoV2("wangwei", "610124199012221003"));
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
