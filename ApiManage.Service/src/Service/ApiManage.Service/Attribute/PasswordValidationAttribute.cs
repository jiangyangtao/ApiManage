using CommonExtention.Core.Extensions;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ApiManage.Service.Attribute
{
    /// <summary>
    /// 密码验证
    /// </summary>
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field, AllowMultiple = false)]
    public class PasswordValidationAttribute : ValidationAttribute
    {
        public PasswordValidationAttribute()
        {

        }

        private string _Message { set; get; }

        public override string FormatErrorMessage(string name) => _Message;

        public override bool IsValid(object value)
        {
            if (value.IsNullOrEmpty())
            {
                _Message = "密码不能为空！";
                return false;
            }
            if (value.ToNotSpaceString().Length < 8)
            {
                _Message = "密码长度不能小于8位数！";
                return false;
            }
            return true;
        }
    }
}
