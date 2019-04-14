using Infrastructure.Utility;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using System.Web;

namespace TC.Web.Utility.Page
{
    /// <summary>
    /// 提供搜索内容查询
    /// </summary>
    public class Searcher
    {
        private HttpContext _context;

        public Searcher(HttpContext context)
        {
            this._context = context;
        }

        /// <summary>
        /// 获取排序字符串
        /// </summary>
        public string OrderBy
        {
            get
            {
                return this.GetValue("OrderBy");
            }
        }

        /// <summary>
        /// 获取Keyword
        /// </summary>
        public string Keyword
        {
            get
            {
                return this.GetValue("Keyword");
            }
        }

        /// <summary>
        /// 获取指定的值
        /// </summary>
        /// <param name="name">名称</param>
        /// <returns></returns>
        public string GetValue(string name)
        {
            var cookie = this._context.Request.Cookies["search"];
            if (cookie == null)
            {
                return null;
            }

            var values = new string[0]; // cookie.Values.GetValues(name);
            if (values == null || values.Length == 0)
            {
                return null;
            }

            var value = HttpUtility.UrlDecode(values.FirstOrDefault()).Trim();
            if (value.IsNullOrEmpty())
            {
                return null;
            }
            return value;
        }

        /// <summary>
        /// 表示默认为true的搜索结果
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public Searcher<T> True<T>()
        {
            return new Searcher<T>(Where.True<T>(), this._context);
        }

        /// <summary>
        /// 表示默认为false的搜索结果
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public Searcher<T> False<T>()
        {
            return new Searcher<T>(Where.True<T>(), this._context);
        }
    }

    /// <summary>
    /// 表示搜索结果
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class Searcher<T>
    {
        private HttpContext _context;

        /// <summary>
        /// 当前表达式
        /// </summary>
        private Expression<Func<T, bool>> exp;

        /// <summary>
        /// 表示搜索结果
        /// </summary>
        /// <param name="trueOrFalse"></param>
        public Searcher(Expression<Func<T, bool>> trueOrFalse, HttpContext httpContext)
        {
            this._context = httpContext;
            this.exp = trueOrFalse;
        }

        /// <summary>
        /// 获取表达式
        /// </summary>
        /// <typeparam name="TKey"></typeparam>
        /// <param name="keySelector"></param>
        /// <param name="op"></param>
        /// <returns></returns>
        private Expression<Func<T, bool>> GetPredicate<TKey>(Expression<Func<T, TKey>> keySelector, Operator op)
        {
            var field = keySelector.Body.OfType<MemberExpression>().Member.Name;
            var value = new Searcher(this._context).GetValue(field);
            if (string.IsNullOrWhiteSpace(value))
            {
                return null;
            }
            else
            {
                var targetValue = Converter.Cast<TKey>(value);
                return Where.GeneratePredicate<T, TKey>(keySelector, targetValue, op);
            }
        }

        /// <summary>
        /// And运算
        /// 文本类型为Contains
        /// 其它类型为Equal
        /// </summary>
        /// <typeparam name="TKey"></typeparam>
        /// <param name="keySelector">健</param>
        /// <returns></returns>
        public Searcher<T> And<TKey>(Expression<Func<T, TKey>> keySelector)
        {
            var op = Operator.Equal;
            if (typeof(TKey) == typeof(string))
            {
                op = Operator.Contains;
            }
            return this.And(keySelector, op);
        }

        /// <summary>
        /// And运算
        /// </summary>
        /// <typeparam name="TKey"></typeparam>
        /// <param name="keySelector">健</param>
        /// <param name="op">操作符</param>
        /// <returns></returns>
        public Searcher<T> And<TKey>(Expression<Func<T, TKey>> keySelector, Operator op)
        {
            var expRight = this.GetPredicate(keySelector, op);
            if (expRight != null)
            {
                this.exp = this.exp.And(expRight);
            }
            return this;
        }

        /// <summary>
        /// Or运算
        /// 文本类型为Contains
        /// 其它类型为Equal
        /// </summary>
        /// <typeparam name="TKey"></typeparam>
        /// <param name="keySelector">健</param>
        /// <returns></returns>
        public Searcher<T> Or<TKey>(Expression<Func<T, TKey>> keySelector)
        {
            var op = Operator.Equal;
            if (typeof(TKey) == typeof(string))
            {
                op = Operator.Contains;
            }

            return this.Or(keySelector, op);
        }

        /// <summary>
        /// Or运算
        /// </summary>
        /// <typeparam name="TKey"></typeparam>
        /// <param name="keySelector">健</param>
        /// <param name="op">操作符</param>
        /// <returns></returns>
        public Searcher<T> Or<TKey>(Expression<Func<T, TKey>> keySelector, Operator op)
        {
            var expRight = this.GetPredicate(keySelector, op);
            if (expRight != null)
            {
                this.exp = this.exp.Or(expRight);
            }
            return this;
        }

        /// <summary>
        /// 转换为条件表达式
        /// </summary>
        /// <returns></returns>
        public Expression<Func<T, bool>> ToExpression()
        {
            return this.exp;
        }

        /// <summary>
        /// 转换为字符串
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return this.exp.ToString();
        }
    }
}
