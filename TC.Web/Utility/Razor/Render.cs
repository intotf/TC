using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Microsoft.AspNetCore.Mvc
{
    /// <summary>
    /// 控制器扩展
    /// </summary>
    public static partial class Render
    {
        /// <summary>
        /// 获取视图的html
        /// </summary>
        /// <param name="controller"></param>
        /// <param name="viewName">View的名称</param>
        /// <param name="model">Model</param>
        /// <returns></returns>
        public static string RenderView(this Controller controller, string viewName, object model)
        {
            var context = controller.ControllerContext;
            if (string.IsNullOrEmpty(viewName))
            {
                viewName = context.RouteData.GetRequiredString("action");
            }

            using (var sw = new StringWriter())
            {
                controller.ViewData.Model = model;
                var viewResult = ViewEngines.Engines.FindPartialView(context, viewName);
                var viewContext = new ViewContext(context, viewResult.View, controller.ViewData, controller.TempData, sw);

                viewResult.View.Render(viewContext, sw);
                return sw.ToString();
            }
        }
    }
}