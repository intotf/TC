using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Web;


namespace Microsoft.AspNetCore.Mvc.Rendering
{
    /// <summary>
    /// HTML扩展
    /// </summary>
    public static partial class HtmlHelperExtend
    {
        /// <summary>
        /// 获取表达式的属性的值和属性名
        /// </summary>
        /// <typeparam name="T">实体类型</typeparam>
        /// <typeparam name="TKey">属性类型</typeparam>
        /// <param name="helper">htmlHelper</param>
        /// <param name="expression">表达式</param>
        /// <param name="name">属性名</param>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="ArgumentException"></exception>
        /// <returns></returns>
        private static TKey GetExpressionValue<T, TKey>(this IHtmlHelper<T> helper, Expression<Func<T, TKey>> expression, out string name)
        {
            if (expression == null)
            {
                throw new ArgumentNullException();
            }

            MemberExpression body = expression.Body as MemberExpression;
            if (body == null || body.Member.DeclaringType.IsAssignableFrom(typeof(T)) == false || body.Expression.NodeType != ExpressionType.Parameter)
            {
                throw new ArgumentException();
            }

            name = body.Member.Name;
            var value = typeof(T).GetProperty(name).GetValue(helper.ViewData.Model, null);
            return (TKey)value;
        }

        /// <summary>
        /// bool类型生成Select下拉框
        /// </summary>
        /// <typeparam name="T">实体类型</typeparam>
        /// <param name="helper">html生成类</param>
        /// <param name="expression">选择器</param>
        /// <param name="trueText">为true时的文本</param>
        /// <param name="falseText">为false时的文本</param>
        /// <param name="htmlAttributes">html属性</param>       
        /// <returns></returns>
        public static IHtmlContent DropDownListFor<T>(this IHtmlHelper<T> helper, Expression<Func<T, bool>> expression, string trueText, string falseText, object htmlAttributes = null)
        {
            string name = string.Empty;
            var value = helper.GetExpressionValue<T, bool>(expression, out name);

            var list = new List<SelectListItem>()
            {
                new SelectListItem() { Value = "true", Text = trueText, Selected = value == true },
                new SelectListItem() { Value = "false", Text = falseText, Selected = value == false }
            };
            return helper.DropDownList(name, list, htmlAttributes);
        }


        /// <summary>
        /// bool类型生成Select下拉框
        /// </summary>
        /// <typeparam name="T">实体类型</typeparam>
        /// <param name="helper">html生成类</param>
        /// <param name="expression">选择器</param>
        /// <param name="trueText">为true时的文本</param>
        /// <param name="falseText">为false时的文本</param>
        /// <param name="htmlAttributes">html属性</param>       
        /// <returns></returns>
        public static IHtmlContent DropDownListFor<T>(this IHtmlHelper<T> helper, Expression<Func<T, bool>> expression, string trueText, string falseText, IDictionary<string, object> htmlAttributes)
        {
            string name = string.Empty;
            var value = helper.GetExpressionValue<T, bool>(expression, out name);

            var list = new List<SelectListItem>()
            {
                new SelectListItem() { Value = "true", Text = trueText, Selected = value == true },
                new SelectListItem() { Value = "false", Text = falseText, Selected = value == false }
            };
            return helper.DropDownList(name, list, htmlAttributes);
        }

        /// <summary>
        /// 生成Select下拉框
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="helper"></param>
        /// <param name="expression">表达式</param>
        /// <param name="selectListItems">选项项</param>
        /// <param name="optionLabel">可行标签</param>
        /// <param name="htmlAttributes">html属性</param>
        /// <returns></returns>
        public static IHtmlContent DropDownListFor<T, TKey>(this IHtmlHelper<T> helper, Expression<Func<T, TKey>> expression, IEnumerable<KeyValuePair<string, string>> selectListItems, string optionLabel, object htmlAttributes)
        {
            var selectList = selectListItems.Select(item => new SelectListItem { Text = item.Value, Value = item.Key });
            return helper.DropDownListFor(expression, selectList, optionLabel, htmlAttributes);
        }

        /// <summary>
        /// 生成Select下拉框
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="helper"></param>
        /// <param name="expression">表达式</param>
        /// <param name="selectListItems">选项项</param>
        /// <param name="optionLabel">可行标签</param>
        /// <param name="htmlAttributes">html属性</param>
        /// <returns></returns>
        public static IHtmlContent DropDownListFor<T, TKey>(this IHtmlHelper<T> helper, Expression<Func<T, TKey>> expression, IEnumerable<KeyValuePair<string, string>> selectListItems, string optionLabel, IDictionary<string, object> htmlAttributes)
        {
            var selectList = selectListItems.Select(item => new SelectListItem { Text = item.Value, Value = item.Key });
            return helper.DropDownListFor(expression, selectList, optionLabel, htmlAttributes);
        }

        /// <summary>
        /// 生成Select下拉框
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="helper"></param>
        /// <param name="expression">表达式</param>
        /// <param name="selectListItems">选项项</param>        
        /// <param name="htmlAttributes">html属性</param>
        /// <returns></returns>
        public static IHtmlContent DropDownListFor<T, TKey>(this IHtmlHelper<T> helper, Expression<Func<T, TKey>> expression, IEnumerable<KeyValuePair<string, string>> selectListItems, IDictionary<string, object> htmlAttributes)
        {
            var selectList = selectListItems.Select(item => new SelectListItem { Text = item.Value, Value = item.Key });
            return helper.DropDownListFor(expression, selectList, htmlAttributes);
        }

        /// <summary>
        /// 生成Select下拉框
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="helper"></param>
        /// <param name="expression">表达式</param>
        /// <param name="selectListItems">选项项</param>       
        /// <param name="htmlAttributes">html属性</param>
        /// <returns></returns>
        public static IHtmlContent DropDownListFor<T, TKey>(this IHtmlHelper<T> helper, Expression<Func<T, TKey>> expression, IEnumerable<KeyValuePair<string, string>> selectListItems, object htmlAttributes)
        {
            var selectList = selectListItems.Select(item => new SelectListItem { Text = item.Value, Value = item.Key });
            return helper.DropDownListFor(expression, selectList, htmlAttributes);
        }

        /// <summary>
        /// 生成Select下拉框
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="helper"></param>
        /// <param name="expression">表达式</param>
        /// <param name="selectListItems">选项项</param>  
        /// <returns></returns>
        public static IHtmlContent DropDownListFor<T, TKey>(this IHtmlHelper<T> helper, Expression<Func<T, TKey>> expression, IEnumerable<KeyValuePair<string, string>> selectListItems)
        {
            var selectList = selectListItems.Select(item => new SelectListItem { Text = item.Value, Value = item.Key });
            return helper.DropDownListFor(expression, selectList);
        }

        /// <summary>
        /// 将html属性合并到字典中
        /// </summary>
        /// <param name="dic">字典</param>
        /// <param name="attribute">属性</param>
        private static void MergenAttribute(IDictionary<string, object> dic, object attribute)
        {
            if (attribute == null)
            {
                return;
            }

            var properties = attribute.GetType().GetProperties();
            foreach (var p in properties)
            {
                var key = p.Name.Replace("_", "-").ToLower();
                var value = p.GetValue(attribute, null);

                if (dic.ContainsKey(key))
                {
                    if (key == "class")
                    {
                        dic[key] = string.Format("{0} {1}", dic[key], value).Trim();
                    }
                }
                else
                {
                    dic.Add(key, value);
                }
            }
        }


        /// <summary>
        /// 只读文本框
        /// </summary>
        /// <typeparam name="TModel"></typeparam>
        /// <typeparam name="TProperty"></typeparam>
        /// <param name="htmlHelper"></param>
        /// <param name="expression"></param>
        /// <param name="htmlAttributes"></param>
        /// <returns></returns>
        public static IHtmlContent TextBoxReadonlyFor<TModel, TProperty>(this IHtmlHelper<TModel> htmlHelper, Expression<Func<TModel, TProperty>> expression, object htmlAttributes)
        {
            var dic = new Dictionary<string, object>();
            MergenAttribute(dic, htmlAttributes);
            MergenAttribute(dic, new { @readonly = "readonly" });
            return htmlHelper.TextBoxFor(expression, dic);
        }

        /// <summary>
        /// 只读文本框
        /// </summary>
        /// <typeparam name="TModel"></typeparam>
        /// <typeparam name="TProperty"></typeparam>
        /// <param name="htmlHelper"></param>
        /// <param name="expression"></param>
        /// <param name="htmlAttributes"></param>
        /// <returns></returns>
        public static IHtmlContent TextBoxReadonlyFor<TModel, TProperty>(this IHtmlHelper<TModel> htmlHelper, Expression<Func<TModel, TProperty>> expression, IDictionary<string, object> htmlAttributes)
        {
            var dic = htmlAttributes == null ? new Dictionary<string, object>() : htmlAttributes;
            MergenAttribute(dic, new { @readonly = "readonly" });
            return htmlHelper.TextBoxFor(expression, dic);
        }
        /// <summary>
        /// 只读文本框
        /// </summary>
        /// <typeparam name="TModel"></typeparam>
        /// <typeparam name="TProperty"></typeparam>
        /// <param name="htmlHelper"></param>
        /// <param name="expression"></param>
        /// <returns></returns>
        public static IHtmlContent TextBoxReadonlyFor<TModel, TProperty>(this IHtmlHelper<TModel> htmlHelper, Expression<Func<TModel, TProperty>> expression)
        {
            return htmlHelper.TextBoxFor(expression, new { @readonly = "readonly" });
        }



        /// <summary>
        /// 从字符串解析为Action
        /// </summary>
        /// <param name="helper"></param>
        /// <param name="keyValues"></param>
        /// <returns></returns>
        public static IHtmlContent ParseAction(this IHtmlHelper helper, string keyValues)
        {
            var datas = keyValues.Matches(@"\w+=\w+").Select(item => item.Split('=')).ToDictionary(kv => kv.First(), kv => (object)kv.Last(), StringComparer.OrdinalIgnoreCase);
            var action = TakeParameter(datas, "action");
            var controller = TakeParameter(datas, "controller");
            return helper.Action(action, controller, new RouteValueDictionary(datas));
        }

        /// <summary>
        /// 获取参数
        /// </summary>
        /// <param name="datas"></param>
        /// <param name="key"></param>
        /// <returns></returns>
        private static string TakeParameter(IDictionary<string, object> datas, string key)
        {
            var value = default(object);
            if (datas.TryGetValue(key, out value))
            {
                datas.Remove(key);
            }
            return value == null ? null : value.ToString();
        }

        #region IHtmlHelper Action 扩展

        public static IHtmlContent Action(this IHtmlHelper helper, string action, object parameters = null)
        {
            var controller = (string)helper.ViewContext.RouteData.Values["controller"];
            return Action(helper, action, controller, parameters);
        }

        public static IHtmlContent Action(this IHtmlHelper helper, string action, string controller, object parameters = null)
        {
            var area = (string)helper.ViewContext.RouteData.Values["area"];

            return Action(helper, action, controller, area, parameters);
        }

        public static IHtmlContent Action(this IHtmlHelper helper, string action, string controller, string area, object parameters = null)
        {
            if (action == null)
                throw new ArgumentNullException("action");

            if (controller == null)
                throw new ArgumentNullException("controller");


            var task = RenderActionAsync(helper, action, controller, area, parameters);

            return task.Result;
        }

        private static async Task<IHtmlContent> RenderActionAsync(this IHtmlHelper helper, string action, string controller, string area, object parameters = null)
        {
            // fetching required services for invocation
            var serviceProvider = helper.ViewContext.HttpContext.RequestServices;
            var actionContextAccessor = helper.ViewContext.HttpContext.RequestServices.GetRequiredService<IActionContextAccessor>();
            var httpContextAccessor = helper.ViewContext.HttpContext.RequestServices.GetRequiredService<IHttpContextAccessor>();
            var actionSelector = serviceProvider.GetRequiredService<IActionSelector>();

            // creating new action invocation context
            var routeData = new RouteData();
            foreach (var router in helper.ViewContext.RouteData.Routers)
            {
                routeData.PushState(router, null, null);
            }
            routeData.PushState(null, new RouteValueDictionary(new { controller = controller, action = action, area = area }), null);
            routeData.PushState(null, new RouteValueDictionary(parameters ?? new { }), null);

            //get the actiondescriptor
            RouteContext routeContext = new RouteContext(helper.ViewContext.HttpContext) { RouteData = routeData };
            var candidates = actionSelector.SelectCandidates(routeContext);
            var actionDescriptor = actionSelector.SelectBestCandidate(routeContext, candidates);

            var originalActionContext = actionContextAccessor.ActionContext;
            var originalhttpContext = httpContextAccessor.HttpContext;
            try
            {
                var newHttpContext = serviceProvider.GetRequiredService<IHttpContextFactory>().Create(helper.ViewContext.HttpContext.Features);
                if (newHttpContext.Items.ContainsKey(typeof(IUrlHelper)))
                {
                    newHttpContext.Items.Remove(typeof(IUrlHelper));
                }
                newHttpContext.Response.Body = new MemoryStream();
                var actionContext = new ActionContext(newHttpContext, routeData, actionDescriptor);
                actionContextAccessor.ActionContext = actionContext;
                var invoker = serviceProvider.GetRequiredService<IActionInvokerFactory>().CreateInvoker(actionContext);
                await invoker.InvokeAsync();
                newHttpContext.Response.Body.Position = 0;
                using (var reader = new StreamReader(newHttpContext.Response.Body))
                {
                    return new HtmlString(reader.ReadToEnd());
                }
            }
            catch (Exception ex)
            {
                return new HtmlString(ex.Message);
            }
            finally
            {
                actionContextAccessor.ActionContext = originalActionContext;
                httpContextAccessor.HttpContext = originalhttpContext;
                if (helper.ViewContext.HttpContext.Items.ContainsKey(typeof(IUrlHelper)))
                {
                    helper.ViewContext.HttpContext.Items.Remove(typeof(IUrlHelper));
                }
            }
        }
        #endregion
    }
}
