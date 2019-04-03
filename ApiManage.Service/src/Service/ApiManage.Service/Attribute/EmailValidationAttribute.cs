using CommonExtention.Core.Extensions;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ApiManage.Service.Attribute
{
    /// <summary>
    /// 邮箱验证
    /// </summary>
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field, AllowMultiple = false)]
    public class EmailValidationAttribute : ValidationAttribute
    {
        public EmailValidationAttribute()
        {

        }

        private string _Message { set; get; }

        public override string FormatErrorMessage(string name) => _Message;

        public override bool IsValid(object value)
        {
            if (value.IsNullOrEmpty())
            {
                _Message = "邮箱不能为空！";
                return false;
            }
            if (!value.ToNotSpaceString().IsEmail())
            {
                _Message = "邮箱格式不正确！";
                return false;
            }
            return true;
        }
    }
}
