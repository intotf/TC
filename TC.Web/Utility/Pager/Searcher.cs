using Infrastructure.Utility;
using Microsoft.AspNetCore.Http;
using PredicateLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Web;

namespace System
{
    /// <summary>
    /// 提供搜索内容查询
    /// </summary>
    public class Searcher
    {
        /// <summary>
        /// Http请求
        /// </summary>
        private readonly IHttpContextAccessor _httpContextAccessor;

        /// <summary>
        /// 初始化
        /// </summary>
        /// <param name="httpContextAccessor"></param>
        /// <returns></returns>
        public static Searcher Init(IHttpContextAccessor httpContextAccessor)
        {
            return new Searcher(httpContextAccessor);
        }

        /// <summary>
        /// 构造函数获取Http 上下文
        /// </summary>
        private Searcher(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        /// <summary>
        /// 获取排序字符串
        /// </summary>
        public string OrderBy
        {
            get
            {
                return GetValue("OrderBy");
            }
        }

        /// <summary>
        /// 获取Keyword
        /// </summary>
        public string Keyword
        {
            get
            {
                return GetValue("Keyword");
            }
        }

        /// <summary>
        /// 获取指定的值
        /// </summary>
        /// <param name="name">名称</param>
        /// <returns></returns>
        public string GetValue(string name)
        {
            var cookies = _httpContextAccessor.HttpContext.Request.Cookies["search"];
            if (cookies == null)
            {
                return null;
            }
            //OR_Name=1&Name=2&Account=3&Mobile=4
            cookies = HttpUtility.UrlDecode(cookies);
            foreach (var cookie in cookies.Split("&"))
            {

                var item = cookie.Split("=");
                if (item[0] == name)
                {
                    return item[1];
                }
            }
            return null;
            //var values = cookie.Values.GetValues(name);
            //if (values == null || values.Length == 0)
            //{
            //    return null;
            //}

            //var value = HttpUtility.UrlDecode(values.FirstOrDefault()).Trim();
            //if (value.IsNullOrEmpty())
            //{
            //    return null;
            //}
            return cookies;
        }

        /// <summary>
        /// 表示默认为true的搜索结果
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public Condition<T> Condition<T>()
        {
            var items = GetConditionItems<T>();
            return new Condition<T>(items);
        }

        /// <summary>
        /// 从Cookie获取搜索条件
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        private IEnumerable<ConditionItem<T>> GetConditionItems<T>()
        {
            foreach (var member in ConditionItem<T>.TypeProperties)
            {
                var value = GetValue(member.Name);
                if (value.IsNullOrEmpty() == false)
                {
                    yield return new ConditionItem<T>(member, value, null);
                }
            }
        }
    }
}
