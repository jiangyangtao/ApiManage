using CommonExtention.Core.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApiManage.Service.Commons
{
    public class Password
    {
        #region 将要加密的字符串进行混淆加密
        /// <summary>
        /// 将要加密的字符串进行混淆加密
        /// </summary>
        /// <param name="value">要加密的字符串</param>
        /// <returns>
        /// 如果 value 参数为 null 或者为空字符串("")，则返回 <see cref="string.Empty"/>；
        /// 否则返回SHA1的混淆加密密文。
        /// </returns>
        public static string CreateConfusionPassword(string value)
        {
            if (value.IsNullOrEmpty()) return string.Empty;
            var s1 = value.ToSHA1Upper();
            var s2 = s1.Substring(6, 31).ToSHA1Upper();
            var s3 = s2.Substring(7, 29).ToSHA1Upper();
            var str = s3.Substring(13, 17) + s2.Substring(18, 20) + s1.Substring(1, 26);
            var s4 = str.ToSHA1Upper();
            var strBuilder = new StringBuilder(s4.Substring(4, 19))
                .Append(s3.Substring(8, 30))
                .Append(s2.Substring(20, 16))
                .Append(s1.Substring(2, 24));
            return strBuilder.ToString();
        }
        #endregion
    }
}
