using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TC.Model;
using TC.Web.Utility;

namespace TC.Web.MvcFilters
{
    /// <summary>
    /// 全局Http 过滤器
    /// </summary>
    public class HttpGlobalExceptionFilter : IExceptionFilter
    {
        /// <summary>
        /// 异常处理
        /// </summary>
        /// <param name="context">异常信息</param>
        public void OnException(ExceptionContext context)
        {
            LogHelper.Error(context.Exception);
            if (context.HttpContext.Request.IsAjax())
            {
                context.Result = new JsonResult(ApiResult.Build<object>().ServiceError());
            }
            //if (context.RouteData.Values["area"].ToString().Equals("Api", System.StringComparison.OrdinalIgnoreCase))
            //{
            //    context.Result = new JsonResult(ApiResult.Build<object>().ServiceError());
            //    //context.HttpContext.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
            //}

            //如果这里设为false，就表示告诉MVC框架，我没有处理这个错误。然后让它跳转到自己定义的错误页（设为true的话，就表示告诉MVC框架，异常我已经处理了。不需要在跳转到错误页了，也部会抛出黄页了）
            context.ExceptionHandled = false;
        }
    }
}
