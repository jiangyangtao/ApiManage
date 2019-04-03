using System;
using System.Collections.Generic;

namespace ApiManage.Service.Models.Entitys
{
    public partial class Interface
    {
        public Guid InterfaceId { get; set; }
        public Guid? ProjectId { get; set; }
        public Guid? DirectoryId { get; set; }
        public Guid? TemplateId { get; set; }
        public string InterfaceName { get; set; }
        public string HttpVersion { get; set; }
        public string RequestUrl { get; set; }
        public string RequestMethod { get; set; }
        public string RequestBody { get; set; }
        public string ContentType { get; set; }
        public string RequestHeader { get; set; }
        public string ResponseHeader { get; set; }
        public string ResponseBody { get; set; }
        public string ResponseCode { get; set; }
        
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
