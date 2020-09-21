using Microsoft.Azure.Cosmos.Table;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Azure.Storage.Models
{
    public class UserInfoV2: TableEntity
    {
        public UserInfoV2()
        {

        }

        public UserInfoV2(string userName, string IdCardNum)
        {
            this.PartitionKey = userName;
            this.RowKey = IdCardNum;

        }

        public string Email { get; set; }

        public string TelNum { get; set; }
    }
}
