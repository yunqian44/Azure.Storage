﻿using Azure.Storage.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Azure.Storage.Service
{
    public interface ITableService
    {
        /// <summary>
        /// AddEntity(abandoned)
        /// </summary>
        /// <param name="user">user</param>
        /// <returns></returns>
        Task AddEntity(UserInfo user);

        /// <summary>
        /// BatchAddEntities(abandoned)
        /// </summary>
        /// <param name="users">users</param>
        /// <returns></returns>
        Task BatchAddEntities(List<UserInfo> users);

        /// <summary>
        /// QueryUsers(abandoned)
        /// </summary>
        /// <param name="filter">filter</param>
        /// <returns></returns>
        IAsyncEnumerable<UserInfo> QueryUsers(string filter);

        /// <summary>
        /// UpdateEntity(abandoned)
        /// </summary>
        /// <param name="user">user</param>
        /// <returns></returns>
        Task UpdateEntity(UserInfo user);

        /// <summary>
        /// DeleteEntity(abandoned)
        /// </summary>
        /// <param name="user">user</param>
        /// <returns></returns>
        Task DeleteEntity(UserInfo user);

        /// <summary>
        /// DeleteTable(abandoned)
        /// </summary>
        /// <param name="tableName">tableName</param>
        /// <returns></returns>
        Task DeleteTable(string tableName);
    }
}
