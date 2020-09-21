using Azure.Storage.Models;
using Microsoft.Azure.Cosmos.Table;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Azure.Storage.Service
{
    public class TableServiceV2 : ITableServiceV2
    {
        private readonly CloudStorageAccount _cloudStorageClient;
        public TableServiceV2(CloudStorageAccount cloudStorageClient)
        {
            _cloudStorageClient = cloudStorageClient;
        }

        #region 01，添加表数据+async Task AddEntity(UserInfo user)
        /// <summary>
        /// 添加表数据
        /// </summary>
        /// <param name="user">用户数据</param>
        /// <returns></returns>
        public async Task AddEntity(UserInfoV2 user)
        {
            var cloudTableClient = _cloudStorageClient.CreateCloudTableClient();
            var cloudTable = cloudTableClient.GetTableReference("USERINFOV2");
            await cloudTable.CreateIfNotExistsAsync();

            var tableOperation = TableOperation.Insert(user);
            await cloudTable.ExecuteAsync(tableOperation);
        } 
        #endregion

        public async Task BatchAddEntities(List<UserInfoV2> users)
        {
            var cloudTableClient = _cloudStorageClient.CreateCloudTableClient();
            var cloudTable = cloudTableClient.GetTableReference("USERINFOV2");
            await cloudTable.CreateIfNotExistsAsync();

            var tableBatchOperation = new TableBatchOperation();
            foreach (UserInfoV2 item in users)
            {
                tableBatchOperation.Insert(item);
            }

            await cloudTable.ExecuteBatchAsync(tableBatchOperation);
        }

        public async Task DeleteEntity(UserInfoV2 user)
        {
            var cloudTableClient = _cloudStorageClient.CreateCloudTableClient();
            var cloudTable = cloudTableClient.GetTableReference("USERINFOV2");

            var queryOperation = TableOperation.Retrieve<UserInfoV2>(user.PartitionKey, user.RowKey);

            var tableResult = await cloudTable.ExecuteAsync(queryOperation);
            if (tableResult.Result is UserInfoV2 userInfo)
            {
                var deleteOperation = TableOperation.Delete(userInfo);
                await cloudTable.ExecuteAsync(deleteOperation);
            }
        }

        public async Task DeleteTable(string tableName)
        {
            var cloudTableClient = _cloudStorageClient.CreateCloudTableClient();
            var cloudTable = cloudTableClient.GetTableReference(tableName);
            await cloudTable.DeleteIfExistsAsync();
        }

        public IEnumerable<UserInfoV2> QueryUsers(string filter)
        {
            var cloudTableClient = _cloudStorageClient.CreateCloudTableClient();
            var cloudTable = cloudTableClient.GetTableReference("USERINFOV2");

            TableQuery<UserInfoV2> query = new TableQuery<UserInfoV2>().Where(filter);

            var users = cloudTable.ExecuteQuery(query);
            foreach (var item in users)
            {
                yield return item;
            }
        }

        public async Task UpdateEntity(UserInfoV2 user)
        {
            var cloudTableClient = _cloudStorageClient.CreateCloudTableClient();
            var cloudTable = cloudTableClient.GetTableReference("USERINFOV2");

            var queryOperation = TableOperation.Retrieve<UserInfoV2>(user.PartitionKey, user.RowKey);

            var tableResult = await cloudTable.ExecuteAsync(queryOperation);
            if (tableResult.Result is UserInfoV2 userInfo)
            {
                user.ETag = userInfo.ETag;
                var deleteOperation = TableOperation.Replace(user);
                await cloudTable.ExecuteAsync(deleteOperation);
            }
        }
    }
}
