using System;
using System.Collections.Generic;

namespace ApiManage.Service.Models.Entitys
{
    public partial class Team
    {
        public Team()
        {
            TeamAccount = new HashSet<TeamAccount>();
        }

        public Guid TeamId { get; set; }
        public string TeamName { get; set; }
        public string TeamIntro { get; set; }

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

        public ICollection<TeamAccount> TeamAccount { get; set; }
    }
}
