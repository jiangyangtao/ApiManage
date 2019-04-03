using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiManage.Service.Commons
{
    public class AppSettings
    {
        /// <summary>
        /// 是否开启跨域
        /// </summary>
        public static bool AllowCross { set; get; }

        /// <summary>
        /// 是否开放注册
        /// </summary>
        public static bool OpenRegister { set; get; }

        /// <summary>
        /// 是否验证注册邮箱
        /// </summary>
        public static bool VerificationRegisterEmail { set; get; }

        /// <summary>
        /// 发送邮件的别称
        /// </summary>
        public static string EmailName { set; get; }

        /// <summary>
        /// 邮箱账号
        /// </summary>
        public static string EmailAccount { set; get; }

        /// <summary>
        /// 邮箱密码
        /// </summary>
        public static string EmailPassword { set; get; }

        /// <summary>
        /// 邮箱 SMTP
        /// </summary>
        public static string EmailHost { set; get; }

        /// <summary>
        /// 邮箱服务端口
        /// </summary>
        public static int EmailPort { set; get; }

        /// <summary>
        /// Token 的有效天数
        /// </summary>
        public static int TokenValidDays { set; get; }

        /// <summary>
        /// Token 续租，是否自动更新 Token 的有效时间
        /// </summary>
        public static bool AutoUpdateTokenValidTime { set; get; }

        /// <summary>
        /// 用于 AES 加密的 Key
        /// </summary>
        public static string AesKey { set; get; }

        /// <summary>
        /// 用于 AES 加密的 向量
        /// </summary>
        public static string AesIv { set; get; }
    }
}
