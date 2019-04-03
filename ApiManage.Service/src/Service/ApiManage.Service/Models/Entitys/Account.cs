using ApiManage.Service.Attribute;
using ApiManage.Service.Filters;
using CommonExtention.Core.Extensions;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace ApiManage.Service.Models.Entitys
{
    public partial class Account
    {
        /// <summary>
        /// 初始化 <see cref="Account"/> 的新实例
        /// </summary>
        public Account()
        {
            TeamAccount = new HashSet<TeamAccount>();
        }

        public Guid AccountId { get; set; }
        public int? AccountType { get; set; }
        public string NickName { get; set; }
        public string AccountName { get; set; }
        public string HeadPic { get; set; }

        [EmailValidation]
        public string Email { get; set; }
        public int? Gender { get; set; }

        [PasswordValidation]
        public string Password { get; set; }
        public int? AccountState { get; set; }
        public int? LoginFailCount { get; set; }

        /// <summary>
        /// 创建用户
        /// </summary>
        public Guid? CreateUser { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime? CreateTime { get; set; }

        /// <summary>
        /// 更新用户
        /// </summary>
        public Guid? UpdateUser { get; set; }

        /// <summary>
        /// 更新时间
        /// </summary>
        public DateTime? UpdateTime { get; set; }

        /// <summary>
        /// 数据状态，0：正常，-1：已删除 
        /// </summary>
        public int? DataState { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        public string Remark { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public ICollection<TeamAccount> TeamAccount { get; set; }
    }
}
