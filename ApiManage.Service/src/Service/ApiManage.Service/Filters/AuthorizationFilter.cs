using ApiManage.Service.Commons;
using ApiManage.Service.Commons.Token;
using ApiManage.Service.Extensions;
using CommonExtention.Core.Common;
using CommonExtention.Core.Extensions;
using CommonExtention.Core.HttpResponseFormat;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.AspNetCore.Mvc.Filters;
using System;

namespace ApiManage.Service.Filters
{
    /// <summary>
    /// 授权验证
    /// </summary>
    public class AuthorizationFilter : IAuthorizationFilter
    {
        public void OnAuthorization(AuthorizationFilterContext context)
        {
            if (!context.Filters.HasFilter(typeof(AllowAnonymousFilter)))
            {
                var result = JsonResultFormat.ResponseFail(-14001, "Invalid token.");
                var authorization = context.HttpContext.Request.Headers.Authorization();
                if (authorization.IsNullOrEmpty())
                {
                    context.Result = result;
                    return;
                }

                var authorizationJson = authorization.ToAesDecrypt(AppSettings.AesKey, AppSettings.AesIv);
                if (authorizationJson.IsNullOrEmpty())
                {
                    context.Result = result;
                    return;
                }

                var token = new Serialization().DeserializeJsonToEntity<AuthorizationToken>(authorizationJson);
                if (token == null)
                {
                    context.Result = result;
                    return;
                }

                if (token.AccountGuid == null || token.AccountGuid == Guid.Empty)
                {
                    context.Result = result;
                    return;
                }

                if (token.ValidTime < DateTime.Now.ToUnixTime())
                {
                    context.Result = result;
                    return;
                }

                if (token.IpAddress != context.HttpContext.Connection.RemoteIpAddress.ToString())
                {
                    context.Result = result;
                    return;
                }

                if (token.UserAgent != context.HttpContext.Request.Headers.UserAgent())
                {
                    context.Result = result;
                    return;
                }

                context.HttpContext.Request.Headers.Add("ValidTime", token.ValidTime.ToString());
                context.HttpContext.Request.Headers["authorization"] = token.AccountGuid.ToString();
            }
        }
    }
}
