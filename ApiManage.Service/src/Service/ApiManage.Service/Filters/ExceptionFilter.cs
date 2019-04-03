using ApiManage.Service.Commons;
using CommonExtention.Core.Common;
using CommonExtention.Core.Extensions;
using CommonExtention.Core.HttpResponseFormat;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Server.Kestrel.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace ApiManage.Service.Filters
{
    /// <summary>
    /// 异常过滤
    /// </summary>
    public class ExceptionFilter : IExceptionFilter
    {
        public void OnException(ExceptionContext context)
        {
            if (context.Exception != null)
            {
                var exception = context.Exception.GetInnerException();
                AsyncLogger.LogException(exception, context.HttpContext.Request);

                var errorCode = exception.HResult;
                if (errorCode == 0) errorCode = 500;
                context.Result = JsonResultFormat.ResponseFail(errorCode, "Server exception.");
                context.ExceptionHandled = true;
            }
        }
    }
}
