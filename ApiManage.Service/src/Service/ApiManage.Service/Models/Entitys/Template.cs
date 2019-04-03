using System;
using System.Collections.Generic;

namespace ApiManage.Service.Models.Entitys
{
    public partial class Template
    {
        public Guid TemplateId { get; set; }
        public Guid? AccountId { get; set; }
        public Guid? ProjectTeamId { get; set; }
        public Guid? ProjectId { get; set; }
        public int? TemplateType { get; set; }
        public bool? IsShare { get; set; }
        public string TemplateContent { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime? CreateTime { get; set; }

        /// <summary>
        /// 创建用户
        /// </summary>
        public Guid? CreateUser { get; set; }

        /// <summary>
        /// 更新时间
        /// </summary>
        public DateTime? UpdateTime { get; set; }

        /// <summary>
        /// 更新用户
        /// </summary>
        public Guid? UpdateUser { get; set; }

        /// <summary>
        /// 数据状态，0：正常，-1：已删除 
        /// </summary>
        public int? DataState { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        public string Remark { get; set; }
    }
}
