using ApiManage.Service.Extensions;
using CommonExtention.Core.Common;
using CommonExtention.Core.Extensions;
using CommonExtention.Core.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using System;
using System.IO;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace ApiManage.Service.Commons
{
    public sealed class Email
    {
        /// <summary>
        /// 异步发送激活邮件
        /// </summary>
        /// <param name="email"></param>
        /// <param name="host"></param>
        /// <returns></returns>
        public async Task<bool> SendActivationEmailAsync(string email, HttpRequest request, RouteData routeData)
        {
            var actionName = routeData.ActionName();
            var controllerName = routeData.ControllerName();
            var host = request.Url().Replace($"{controllerName}/{actionName}", "");
            var key = $"{DateTime.Now.ToUnixTime() + 3600}|{email}".ToAesEncrypt(AppSettings.AesKey.Substring(0, 16));
            var url = $"{host}/home/activationemail?key={key}";
            var content = ActivationTemplate.Replace("$url", url);
            return await SendMailAsync(email, content, request, true);
        }

        /// <summary>
        /// 激活邮件模板
        /// </summary>
        public string ActivationTemplate
        {
            get => File.ReadAllText($@"{Directory.GetCurrentDirectory()}\wwwroot\Template\ActivationTemplate.txt", Encoding.UTF8);
        }

        /// <summary>
        /// 异步发送邮件
        /// </summary>
        /// <param name="targetMail">目标邮箱</param>
        /// <param name="content">发送内容</param>
        /// <param name="request"><see cref="HttpRequest"/> 对象</param>
        /// <returns>true 则发送成功；false 则发送失败，并将异常写入到日志。</returns>
        public async Task<bool> SendMailAsync(string targetMail, string content, HttpRequest request, bool isHtml = false)
        {
            try
            {
                var email = new CommonExtention.Core.Common.Email
                {
                    ServiceConfig = new EmailServiceConfig()
                    {
                        Host = AppSettings.EmailHost,
                        Port = AppSettings.EmailPort,
                        EmailAddress = AppSettings.EmailAccount,
                        Password = AppSettings.EmailPassword,
                    },

                    EmailContent = new EmailContent()
                    {
                        ReplyAddress = new MailAddress(AppSettings.EmailAccount),
                        Title = "ApiManage 激活邮件",
                        IsHtmlContent = true,
                        Body = content,
                    }
                };
                return await email.SendEmailAsync(new MailAddress(targetMail));
            }
            catch (Exception e)
            {
                AsyncLogger.LogException(e, request);
                return false;
            }
        }
    }
}
