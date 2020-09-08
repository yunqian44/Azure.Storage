using Azure.Storage.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Azure.Storage.Service
{
    public interface ITableService
    {
        Task AddEntity(UserInfo user);

        Task BatchAddEntities(List<UserInfo> users);

        IAsyncEnumerable<UserInfo> QueryUsers(string filter);

        Task UpdateEntity(UserInfo user);

        Task DeleteEntity(UserInfo user);

        Task DeleteTable(string tableName);

    }
}
