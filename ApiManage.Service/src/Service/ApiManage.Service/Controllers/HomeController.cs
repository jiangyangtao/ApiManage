using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ApiManage.Service.Commons;
using ApiManage.Service.Extensions;
using ApiManage.Service.Filters;
using ApiManage.Service.Models;
using CommonExtention.Core.Extensions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ApiManage.Service.Controllers
{
    public class HomeController : BaseController
    {
        [AllowAnonymous]
        public IActionResult Index()
        {
            var ip = HttpContext.Connection.RemoteIpAddress.ToString();
            return ResponseSuccess(ip);
        }

        [HttpPost]
        public IActionResult Authorization() => ResponseSuccess();

        public async Task<IActionResult> SendActivationMail()
        {
            var state = await new Email().SendActivationEmailAsync(BaseAccount.Email, HttpContext.Request, RouteData);
            if (!state) return ResponseFail(-1, "邮件发送失败，请联系管理员！");
            return ResponseSuccess();
        }

        [AllowAnonymous]
        public async Task<IActionResult> ActivationEmail(string key)
        {
            if (key.IsNullOrEmpty()) return NotFound();

            var plaintext = key.ToAesDecrypt(AppSettings.AesKey.Substring(0, 16));
            if (plaintext.IsNullOrEmpty()) return NotFound();

            var plaintextArray = plaintext.Split("|");
            if (plaintextArray.Length <= 1) return NotFound();

            var limitTime = plaintextArray[0];
            if (limitTime.IsNullOrEmpty()) return NotFound();

            var limit = limitTime.ToInt64();
            if (limit < DateTime.Now.ToUnixTime()) return NotFound();

            var email = plaintextArray[1];
            if (email.IsNullOrEmpty()) return NotFound();

            using (var Db = new ApiManageContext())
            {
                var account = await Db.Account.FirstOrDefaultAsync(a => a.Email == email);
                if (account == null) return NotFound();
                if (account.AccountState != -1) return NotFound();

                account.AccountState = 0;
                await UpdateAndSaveAsync(account);
            }
            return Content("邮箱激活成功！");
        }
    }
}