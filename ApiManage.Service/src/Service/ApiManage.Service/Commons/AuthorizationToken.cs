using ApiManage.Service.Extensions;
using CommonExtention.Core.Common;
using CommonExtention.Core.Extensions;
using CommonExtention.Core.HttpResponseFormat;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiManage.Service.Commons.Token
{
    public sealed class AuthorizationToken
    {
        public Guid AccountGuid { set; get; }

        public long ValidTime { set; get; }

        public string IpAddress { set; get; }

        public string UserAgent { set; get; }
    }
}
