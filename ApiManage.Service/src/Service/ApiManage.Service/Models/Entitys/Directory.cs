using System;
using System.Collections.Generic;

namespace ApiManage.Service.Models.Entitys
{
    /// <summary>
    /// 
    /// </summary>
    public partial class Directory
    {
        /// <summary>
        /// 目录ID
        /// </summary>
        public Guid DirectoryId { get; set; }

        /// <summary>
        /// 项目ID
        /// </summary>
        public Guid? ProjectId { get; set; }

        /// <summary>
        /// 目录名称
        /// </summary>
        public string DirectoryName { get; set; }

        /// <summary>
        /// 上级目录
        /// </summary>
        public Guid? ParentDirectoryId { get; set; }

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
    }
}
