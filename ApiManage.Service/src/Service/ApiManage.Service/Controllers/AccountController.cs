using ApiManage.Service.Commons;
using ApiManage.Service.Commons.Token;
using ApiManage.Service.Models;
using ApiManage.Service.Models.Entitys;
using CommonExtention.Core.Common;
using CommonExtention.Core.Extensions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;

namespace ApiManage.Service.Controllers
{
    public class AccountController : BasicsController
    {
        /// <summary>
        /// 登录
        /// </summary>
        /// <param name="email"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        [AllowAnonymous]
        public async Task<IActionResult> Login(Account user)
        {
            using (var Db = new ApiManageContext())
            {
                var account = await Db.Account.FirstOrDefaultAsync(a => a.Email == user.Email);
                if (account == null) return ResponseFail(-4, "未找到你的账号！");
                if (account.LoginFailCount == 10) return ResponseFail(-5, "你的账号已被锁定，请联系系统管理员！");

                var _password = Password.CreateConfusionPassword(user.Password);
                var ipAddress = HttpContext.Connection.RemoteIpAddress.ToString();
                var userAgent = HttpContext.Request.Headers.UserAgent();
                var loginRecord = new LoginRecord()
                {
                    AccountId = account.AccountId,
                    IpAddress = ipAddress,
                    UserAgent = userAgent,
                    LoginState = 1,
                };

                if (account.Password != _password)
                {
                    account.LoginFailCount += 1;
                    Db.Account.Update(account);

                    loginRecord.LoginState = -1;
                    Db.LoginRecord.Add(loginRecord);
                    await Db.SaveChangesAsync();

                    var errorText = "账号或密码错误！";
                    var chanceNumber = 10 - account.LoginFailCount;
                    if (account.LoginFailCount > 4) errorText = $"账号或密码错误！你还有{chanceNumber}次机会！";
                    if (chanceNumber == 0) errorText = "你的账号已被锁定，请联系系统管理员！";
                    return ResponseFail(-6, errorText);
                }

                Db.LoginRecord.Add(loginRecord);
                account.LoginFailCount = 0;
                Db.Account.Update(account);
                await Db.SaveChangesAsync();

                var authorization = new AuthorizationToken()
                {
                    AccountGuid = account.AccountId,
                    ValidTime = DateTime.Now.ToUnixTime() + (AppSettings.TokenValidDays * 86400),
                    IpAddress = ipAddress,
                    UserAgent = userAgent
                };
                var authorizationJson = new Serialization().SerializeEntityToJson(authorization);
                var token = authorizationJson.ToAesEncrypt(AppSettings.AesKey, AppSettings.AesIv);
                return ResponseSuccess(token);
            }
        }

        /// <summary>
        /// 注册
        /// </summary>
        /// <param name="email"></param>
        /// <param name="name"></param>
        /// <param name="gender"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        [AllowAnonymous]
        public async Task<IActionResult> Register(string email, string name, int? gender, string password)
        {
            using (var Db = new ApiManageContext())
            {
                var exist = await Db.Account.AnyAsync(a => a.Email == email);
                if (exist) return ResponseFail(-4, "账号已存在！");

                if (AppSettings.VerificationRegisterEmail)
                {
                    var state = await new Commons.Email().SendActivationEmailAsync(email, HttpContext.Request, RouteData);
                    if (!state) return ResponseFail(-5, "邮件发送失败，请联系管理员！");
                }

                var account = new Account()
                {
                    Email = email,
                    NickName = name,
                    Gender = gender,
                    Password = Password.CreateConfusionPassword(password),
                    AccountState = 0,
                };
                var successText = "注册成功！";
                if (AppSettings.VerificationRegisterEmail)
                {
                    account.AccountState = -1;
                    successText = $"注册成功！激活邮件已发送到{email},请注意查收！";
                }
                Db.Account.Add(account);

                Task.WaitAll(await new Commons.Email().SendActivationEmailAsync(email, HttpContext.Request, RouteData), await Db.SaveChangesAsync());

                ;
                return ResponseSuccess(successText);
            }
        }
    }
}