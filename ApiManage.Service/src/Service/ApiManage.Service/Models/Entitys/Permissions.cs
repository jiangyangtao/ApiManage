using System;
using System.Collections.Generic;

namespace ApiManage.Service.Models.Entitys
{
    public partial class Permissions
    {
        public Guid PermissionsId { get; set; }
        public Guid? ProjectAccountId { get; set; }
        public Guid? RoleId { get; set; }
        public int? PermissionsType { get; set; }
        public bool? AllowCreateInterface { get; set; }
        public bool? AllowCreateDirectory { get; set; }
        public bool? AllowCreateTemplate { get; set; }
        public bool? AllowCreateProjectTemplate { get; set; }
        public bool? AllowCreateTeamTemplate { get; set; }
        public bool? AllowCreatePersonalTemplate { get; set; }
        public bool? AllowCreateAccount { get; set; }
        public bool? AllowCreateInviteAccount { get; set; }
        public bool? AllowCreateRole { get; set; }
        public bool? AllowAllocationRole { get; set; }
        public bool? AllowDeleteInterface { get; set; }
        public bool? AllowDeleteOthersInterface { get; set; }
        public bool? AllowDeleteDirectory { get; set; }
        public bool? AllowDeleteOthersDirectory { get; set; }
        public bool? AllowDeleteTemplate { get; set; }
        public bool? AllowDeleteProjectTemplate { get; set; }
        public bool? AllowDeleteTeamTemplate { get; set; }
        public bool? AllowRemoveAccount { get; set; }
        public bool? AllowDeleteRole { get; set; }
        public bool? AllowModifyOthersRole { get; set; }
        public bool? AllowModifyOthersDirectory { get; set; }
        public bool? AllowModifyTemplate { get; set; }
        public bool? AllowModifyProjectTemplate { get; set; }
        public bool? AllowModifyTeamTemplate { get; set; }
        public bool? AllowModifyOthersTemplate { get; set; }
        public bool? AllowViewInterface { get; set; }

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
