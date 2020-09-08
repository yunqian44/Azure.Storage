using Microsoft.WindowsAzure.Storage.Table;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Azure.Storage.Models
{
    public class UserInfo:TableEntity
    {
        public UserInfo()
        {

        }

        public UserInfo(string userName, string IdCardNum)
        {
            this.PartitionKey = userName;
            this.RowKey = IdCardNum;

        }

        public string Email { get; set; }

        public string TelNum { get; set; }
    }
}
