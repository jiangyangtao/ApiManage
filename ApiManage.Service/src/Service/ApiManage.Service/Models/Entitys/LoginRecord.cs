using System;
using System.Collections.Generic;

namespace ApiManage.Service.Models.Entitys
{
    public partial class LoginRecord
    {
        public Guid LoginRecordId { get; set; }
        public Guid AccountId { get; set; }
        public DateTime? LoginTime { get; set; }
        public string IpAddress { get; set; }
        public string UserAgent { get; set; }
        public int? LoginState { get; set; }
    }
}
