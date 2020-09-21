using Azure.Storage.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Azure.Storage.Service
{
    public interface ITableServiceV2
    {
        /// <summary>
        /// AddEntity(abandoned)
        /// </summary>
        /// <param name="user">user</param>
        /// <returns></returns>
        Task AddEntity(UserInfoV2 user);

        /// <summary>
        /// BatchAddEntities(abandoned)
        /// </summary>
        /// <param name="users">users</param>
        /// <returns></returns>
        Task BatchAddEntities(List<UserInfoV2> users);

        /// <summary>
        /// QueryUsers(abandoned)
        /// </summary>
        /// <param name="filter">filter</param>
        /// <returns></returns>
        IEnumerable<UserInfoV2> QueryUsers(string filter);

        /// <summary>
        /// UpdateEntity(abandoned)
        /// </summary>
        /// <param name="user">user</param>
        /// <returns></returns>
        Task UpdateEntity(UserInfoV2 user);

        /// <summary>
        /// DeleteEntity(abandoned)
        /// </summary>
        /// <param name="user">user</param>
        /// <returns></returns>
        Task DeleteEntity(UserInfoV2 user);

        /// <summary>
        /// DeleteTable(abandoned)
        /// </summary>
        /// <param name="tableName">tableName</param>
        /// <returns></returns>
        Task DeleteTable(string tableName);
    }
}
