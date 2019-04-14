using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Linq;

namespace TC.Web.Utility.Menu
{
    /// <summary>
    /// 权限应用特性
    /// </summary>
    [AttributeUsage(AttributeTargets.Method | AttributeTargets.Class, Inherited = true, AllowMultiple = false)]
    public sealed class PermissionAttribute : Attribute, IActionFilter
    {
        /// <summary>
        /// 处理权限
        /// </summary>
        /// <param name="topNode"></param>
        private void SetPermission(ActionExecutingContext filterContext, MenuNode<Actions> topNode)
        {
            var controller = filterContext.Controller as IManageController;
            if (controller == null)
            {
                return;
            }

            if (controller.LoginRole == Role.SuperAdmin)
            {
                topNode.SetPermission(true);
            }
            else
            {
                using (var db = new SqlDb())
                {
                    var pNodes = db.OrganizePermission
                        .Where(item => item.OrganizeID == controller.UserInfo.OR_ID)
                        .Select(item => new { item.AllowedAction, item.HashMd5 })
                        .ToArray()
                        .Select(item => new PermissionNode<Actions> { AllowedAction = (Actions)item.AllowedAction, HashMd5 = item.HashMd5 })
                        .ToArray();

                    topNode.SetPermission(pNodes);
                }
            }
        }


        /// <summary>
        /// Action执行前
        /// </summary>
        /// <param name="filterContext"></param>
        public void OnActionExecuting(ActionExecutingContext filterContext)
        {
            var topNode = SystemMenu.Create();
            SystemMenu.TopNode = topNode;

            this.SetPermission(filterContext, topNode);

            SystemMenu.ActiveNode = PermissionProcesser.Process<Actions>(filterContext.RouteData, topNode, (currentNode) =>
            {
                if (filterContext.RequestContext.HttpContext.Request.IsAjaxRequest())
                {
                    var message = currentNode.IsPageNode ? "您无权限查看此页面" : "您无权限操作该功能";
                    filterContext.HttpContext.Response.StatusCode = 500;
                    filterContext.Result = new ContentResult { Content = message };
                }
                else
                {
                    filterContext.Result = new RedirectResult("/manage");
                }
            });
        }

        /// <summary>
        /// Action执行后
        /// </summary>
        /// <param name="filterContext"></param>
        public void OnActionExecuted(ActionExecutedContext filterContext)
        {
        }
    }
}