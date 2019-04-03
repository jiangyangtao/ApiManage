using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiManage.Service.Extensions
{
    public static class IHeaderDictionaryExtensions
    {
        public static string Authorization(this IHeaderDictionary keyValues)
        {
            if (keyValues == null || !keyValues.ContainsKey("authorization")) return string.Empty;

            return keyValues["authorization"];
        }
    }
}
