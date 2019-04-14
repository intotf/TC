using Infrastructure.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TC.Model.Entitys;

namespace TC.Web.Utility
{
    /// <summary>
    /// 表示共用内存缓存
    /// </summary>
    public class WebCache
    {
        /// <summary>
        /// 验证码 Cookies Key 名称
        /// </summary>
        public static string codeCookieKey = "codeToken";

        /// <summary>
        /// 验证码缓存 默认1分钟
        /// </summary>
        public static CacheCliens<string> codeCache = new CacheCliens<string>(TimeSpan.FromMinutes(1));

        /// <summary>
        /// 管理员登录缓存 1天
        /// </summary>
        public static CacheCliens<UserInfo> userCache = new CacheCliens<UserInfo>(TimeSpan.FromDays(1));

        /// <summary>
        /// 管理员登录Cookies Key名称
        /// </summary>
        public static string userCookieKey = "userToken";
    }
}
