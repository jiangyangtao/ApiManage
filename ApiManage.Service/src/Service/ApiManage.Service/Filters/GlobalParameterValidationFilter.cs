using CommonExtention.Core.HttpResponseFormat;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace ApiManage.Service.Filters
{
    /// <summary>
    /// 全局参数验证
    /// </summary>
    public class GlobalParameterValidationFilter : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            if (context.ModelState.IsValid == false)
            {
                var message = string.Empty;
                var modelErrors = context.ModelState.Values.Select(x => x.Errors);
                foreach (var item in modelErrors)
                {
                    foreach (var subItem in item)
                    {
                        message = subItem.ErrorMessage;
                    }
                }
                context.Result = JsonResultFormat.ResponseFail(-1, message);
            }
            base.OnActionExecuting(context);
        }
    }
}
