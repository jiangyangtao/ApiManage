using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ApiManage.Service.Commons;
using ApiManage.Service.Models;
using ApiManage.Service.Models.Entitys;
using CommonExtention.Core.Common;
using CommonExtention.Core.Extensions;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.EntityFrameworkCore;
using CommonExtention.Core.HttpResponseFormat;
using ApiManage.Service.Extensions;
using ApiManage.Service.Commons.Token;

namespace ApiManage.Service.Controllers
{
    [EnableCors("default")]
    public class BaseController : BasicsController
    {
        #region Init
        /// <summary>
        /// DataBase
        /// </summary>
        protected readonly ApiManageContext Db;

        protected Guid BaseAccountID { get; private set; }

        /// <summary>
        /// Base User
        /// </summary>
        protected Account BaseAccount { get; private set; }

        private long _ValidTime { set; get; }

        /// <summary>
        /// 
        /// </summary>
        public BaseController()
        {
            Db = new ApiManageContext();
        }

        /// <summary>
        /// 释放资源
        /// </summary>
        /// <param name="disposing"></param>
        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);
            Db.Dispose();
        }
        #endregion

        #region Filter

        /// <summary>
        /// 当前请求的 Action 是否需要身份验证
        /// </summary>
        private bool _IsAuthorization { set; get; }


        public override void OnActionExecuting(ActionExecutingContext context)
        {
            _IsAuthorization = context.Filters.HasFilter(typeof(AllowAnonymousFilter));
            if (!_IsAuthorization)
            {
                var uid = context.HttpContext.Request.Headers.Authorization();
                var accountID = uid.ToGuid();
                var account = Db.Account.AsNoTracking().FirstOrDefault(a => a.AccountId == accountID && a.DataState == 0);
                if (account == null)
                {
                    context.Result = JsonResultFormat.ResponseFail(14002, "Authorization failure.");
                    return;
                }

                if (account.LoginFailCount == 10)
                {
                    context.Result = JsonResultFormat.ResponseFail(14003, "The account has been locked.");
                    return;
                }

                _ValidTime = context.HttpContext.Request.Headers["ValidTime"].ToString().ToInt64();
                BaseAccountID = account.AccountId;
                BaseAccount = account;
            }
            base.OnActionExecuting(context);
        }

        public override void OnActionExecuted(ActionExecutedContext context)
        {
            var time = DateTime.Now.ToUnixTime() + (AppSettings.TokenValidDays * 86400);
            if (!_IsAuthorization && time > _ValidTime)
            {
                var authorization = new AuthorizationToken()
                {
                    AccountGuid = BaseAccountID,
                    ValidTime = DateTime.Now.ToUnixTime() + (AppSettings.TokenValidDays * 86400),
                    IpAddress = HttpContext.Connection.RemoteIpAddress.ToString(),
                    UserAgent = HttpContext.Request.Headers.UserAgent()
                };
                var authorizationJson = new Serialization().SerializeEntityToJson(authorization);
                var token = authorizationJson.ToAesEncrypt(AppSettings.AesKey, AppSettings.AesIv);
                context.HttpContext.Response.Headers.Add("token", "token");
            }
            base.OnActionExecuted(context);
        }

        #endregion

        #region Action

        #region Add

        #region AddAndSave
        /// <summary>
        /// 添加并且保存 - 同步
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <param name="entity"></param>
        /// <returns></returns>
        protected int AddAndSave<TEntity>(TEntity entity) where TEntity : class
        {
            if (entity == null) return 0;

            Db.Add(entity);
            return Db.SaveChanges();
        }
        #endregion

        #region AddAndSaveAsync
        /// <summary>
        /// 添加并且保存 - 异步
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <param name="entity"></param>
        /// <returns></returns>
        protected async Task<int> AddAndSaveAsync<TEntity>(TEntity entity) where TEntity : class
        {
            if (entity == null) return 0;

            Db.Add(entity);
            return await Db.SaveChangesAsync();
        }
        #endregion

        #region AddRangeAndSave
        /// <summary>
        /// 批量添加并且保存 - 同步
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <param name="entities"></param>
        /// <returns></returns>
        protected int AddRangeAndSave<TEntity>(List<TEntity> entities) where TEntity : class
        {
            if (entities == null || entities.Count == 0) return 0;

            Db.AddRange(entities);
            return Db.SaveChanges();
        }
        #endregion

        #region AddRangeAndSaveAsync
        /// <summary>
        /// 批量添加并且保存 - 异步
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <param name="entities"></param>
        /// <returns></returns>
        public async Task<int> AddRangeAndSaveAsync<TEntity>(List<TEntity> entities) where TEntity : class
        {
            if (entities == null || entities.Count == 0) return 0;

            await Db.AddRangeAsync(entities);
            return await Db.SaveChangesAsync();
        }
        #endregion

        #endregion

        #region Delete

        #region DeleteAndSave
        /// <summary>
        /// 删除并且保存 - 同步
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <param name="entity"></param>
        /// <returns></returns>
        protected int DeleteAndSave<TEntity>(TEntity entity) where TEntity : class
        {
            if (entity == null) return 0;

            Db.Remove(entity);
            return Db.SaveChanges();
        }
        #endregion

        #region DeleteAndSaveAsync
        /// <summary>
        /// 删除并且保存 - 异步
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <param name="entity"></param>
        /// <returns></returns>
        protected async Task<int> DeleteAndSaveAsync<TEntity>(TEntity entity) where TEntity : class
        {
            if (entity == null) return 0;

            Db.Remove(entity);
            return await Db.SaveChangesAsync();
        }
        #endregion

        #region DeleteRangeAndSave
        /// <summary>
        /// 批量删除并且保存 - 同步
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <param name="entities"></param>
        /// <returns></returns>
        protected int DeleteRangeAndSave<TEntity>(List<TEntity> entities) where TEntity : class
        {
            if (entities == null || entities.Count == 0) return 0;

            Db.RemoveRange(entities);
            return Db.SaveChanges();
        }
        #endregion

        #region DeleteRangeAndSaveAsync
        /// <summary>
        /// 批量删除并且保存 - 异步
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <param name="entities"></param>
        /// <returns></returns>
        protected async Task<int> DeleteRangeAndSaveAsync<TEntity>(List<TEntity> entities) where TEntity : class
        {
            if (entities == null || entities.Count == 0) return 0;

            Db.RemoveRange(entities);
            return await Db.SaveChangesAsync();
        }
        #endregion

        #endregion

        #region Update

        #region UpdateAndSave
        /// <summary>
        /// 更新并且保存 - 同步
        /// </summary>
        /// <typeparam name="TEntiey"></typeparam>
        /// <param name="entity"></param>
        /// <returns></returns>
        public int UpdateAndSave<TEntiey>(TEntiey entity) where TEntiey : class
        {
            if (entity == null) return 0;

            Db.Update(entity);
            return Db.SaveChanges();
        }
        #endregion

        #region UpdateAndSaveAsync
        /// <summary>
        /// 更新并且保存 - 异步
        /// </summary>
        /// <typeparam name="TEntiey"></typeparam>
        /// <param name="entity"></param>
        /// <returns></returns>
        public async Task<int> UpdateAndSaveAsync<TEntiey>(TEntiey entity) where TEntiey : class
        {
            if (entity == null) return 0;

            Db.Update(entity);
            return await Db.SaveChangesAsync();
        }
        #endregion

        #region UpdateRangeAndSave
        /// <summary>
        /// 批量更新并且保存 - 同步
        /// </summary>
        /// <typeparam name="TEntiey"></typeparam>
        /// <param name="entities"></param>
        /// <returns></returns>
        public int UpdateRangeAndSave<TEntiey>(List<TEntiey> entities) where TEntiey : class
        {
            if (entities == null || entities.Count == 0) return 0;

            Db.UpdateRange(entities);
            return Db.SaveChanges();
        }
        #endregion

        #region UpdateRangeAndSaveAsync
        /// <summary>
        /// 批量更新并且保存 - 异步
        /// </summary>
        /// <typeparam name="TEntiey"></typeparam>
        /// <param name="entities"></param>
        /// <returns></returns>
        public async Task<int> UpdateRangeAndSaveAsync<TEntiey>(List<TEntiey> entities) where TEntiey : class
        {
            if (entities == null || entities.Count == 0) return 0;

            Db.UpdateRange(entities);
            return await Db.SaveChangesAsync();
        }
        #endregion

        #endregion

        #endregion
    }
}