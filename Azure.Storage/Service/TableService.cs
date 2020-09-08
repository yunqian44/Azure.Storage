using Azure.Storage.Models;
using Microsoft.VisualBasic;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Table;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Azure.Storage.Service
{
    public class TableService : ITableService
    {
        private readonly CloudStorageAccount _cloudStorageClient;
        public TableService(CloudStorageAccount cloudStorageClient)
        {
            _cloudStorageClient = cloudStorageClient;
        }

        #region 01，添加表数据+async Task AddEntity(UserInfo user)
        /// <summary>
        /// 添加表数据
        /// </summary>
        /// <param name="user">用户数据</param>
        /// <returns></returns>
        public async Task AddEntity(UserInfo user)
        {
            var cloudTableClient = _cloudStorageClient.CreateCloudTableClient();
            var cloudTable = cloudTableClient.GetTableReference("USERINFO");
            await cloudTable.CreateIfNotExistsAsync();

            var tableOperation = TableOperation.Insert(user);
            await cloudTable.ExecuteAsync(tableOperation);
        }
        #endregion

        #region 02，批量添加用户表数据+async Task BatchAddEntities(List<UserInfo> users)
        /// <summary>
        /// 批量添加用户表数据
        /// </summary>
        /// <param name="users">用户数据</param>
        /// <returns></returns>
        public async Task BatchAddEntities(List<UserInfo> users)
        {
            var cloudTableClient = _cloudStorageClient.CreateCloudTableClient();
            var cloudTable = cloudTableClient.GetTableReference("USERINFO");
            await cloudTable.CreateIfNotExistsAsync();

            var tableBatchOperation = new TableBatchOperation();
            foreach (UserInfo item in users)
            {
                tableBatchOperation.Insert(item);
            }

            await cloudTable.ExecuteBatchAsync(tableBatchOperation);
        }
        #endregion

        #region 03，删除表操作根据表名+async Task DeleteTable(string tableName)
        /// <summary>
        /// 删除表操作根据表名
        /// </summary>
        /// <param name="tableName">表命</param>
        /// <returns></returns>
        public async Task DeleteTable(string tableName)
        {
            var cloudTableClient = _cloudStorageClient.CreateCloudTableClient();
            var cloudTable = cloudTableClient.GetTableReference(tableName);
            await cloudTable.DeleteIfExistsAsync();
        }
        #endregion

        #region 04，删除用户数据根据用户条件+async Task DeleteEntity(UserInfo user)
        /// <summary>
        /// 删除用户数据根据用户条件
        /// </summary>
        /// <param name="user">用户条件</param>
        /// <returns></returns>
        public async Task DeleteEntity(UserInfo user)
        {
            var cloudTableClient = _cloudStorageClient.CreateCloudTableClient();
            var cloudTable = cloudTableClient.GetTableReference("USERINFO");

            var queryOperation = TableOperation.Retrieve<UserInfo>(user.PartitionKey, user.RowKey);

            var tableResult = await cloudTable.ExecuteAsync(queryOperation);
            if (tableResult.Result is UserInfo userInfo)
            {
                var deleteOperation = TableOperation.Delete(userInfo);
                await cloudTable.ExecuteAsync(deleteOperation);
            }
        }
        #endregion

        #region 05，查询用户根据条件+async IAsyncEnumerable<UserInfo> QueryUsers(string filter)
        /// <summary>
        /// 查询用户根据条件
        /// </summary>
        /// <param name="filter">条件</param>
        /// <returns></returns>
        public async IAsyncEnumerable<UserInfo> QueryUsers(string filter)
        {
            var cloudTableClient = _cloudStorageClient.CreateCloudTableClient();
            var cloudTable = cloudTableClient.GetTableReference("USERINFO");

            TableQuery<UserInfo> query = new TableQuery<UserInfo>().Where(filter);

            var users = await cloudTable.ExecuteQuerySegmentedAsync<UserInfo>(query, null);
            foreach (var item in users)
            {
                yield return item;
            }
        }
        #endregion

        #region 06，更新用户表数据根据新的用户数据+async Task UpdateEntity(UserInfo user)
        /// <summary>
        /// 更新用户表数据根据新的用户数据
        /// </summary>
        /// <param name="user">新用户数据</param>
        /// <returns></returns>
        public async Task UpdateEntity(UserInfo user)
        {
            var cloudTableClient = _cloudStorageClient.CreateCloudTableClient();
            var cloudTable = cloudTableClient.GetTableReference("USERINFO");

            var queryOperation = TableOperation.Retrieve<UserInfo>(user.PartitionKey, user.RowKey);

            var tableResult = await cloudTable.ExecuteAsync(queryOperation);
            if (tableResult.Result is UserInfo userInfo)
            {
                user.ETag = userInfo.ETag;
                var deleteOperation = TableOperation.Replace(user);
                await cloudTable.ExecuteAsync(deleteOperation);
            }
        } 
        #endregion
    }
}
