using System;
using TC.Web.Utility.Menu;

namespace Microsoft.AspNetCore.Mvc.Rendering
{
    /// <summary>
    /// HtmlHelper扩展
    /// </summary>
    public static partial class HtmlHelperExtend
    {
        /// <summary>
        /// 是否允许至少一种操作行为
        /// </summary>
        /// <param name="html">htmlHelper</param>
        /// <returns></returns>
        public static bool AllowAction(this IHtmlHelper html)
        {
            //var currentNode = SystemMenu.ActiveNode;
            return true;// currentNode.AllowedAction.GetHashCode() > 0;
        }

        /// <summary>
        /// 是否允许某种操作行为
        /// </summary>
        /// <param name="html">htmlHelper</param>
        /// <param name="actionEnum">允许的操作行为</param>       
        /// <returns></returns>
        public static bool AllowAction(this IHtmlHelper html, Actions actionEnum)
        {
            return true;
            //var currentNode = SystemMenu.ActiveNode;
            //if (currentNode == null)
            //{
            //    throw new NotSupportedException();
            //}
            //return currentNode.IsAllow(actionEnum);
        }

        /// <summary>
        /// 是否允许某种操作行为
        /// </summary>
        /// <param name="html">htmlHelper</param>
        /// <param name="actionEnum">允许的操作行为</param>
        /// <param name="controller">控制器</param>
        /// <param name="action">action名</param>
        /// <returns></returns>
        public static bool AllowAction(this IHtmlHelper html, Actions actionEnum, string controller, string action)
        {
            return true;
            //var nodes = SystemMenu.TopNode;
            //var node = nodes.FindPageNode(controller, action);
            //if (node == null)
            //{
            //    throw new NotSupportedException();
            //}
            //return node.IsAllow(actionEnum);
        }

        /// <summary>
        /// 是否允许某种操作行为
        /// </summary>
        /// <param name="html">htmlHelper</param>
        /// <param name="actionEnum">允许的操作行为</param>
        /// <param name="name">节点名称</param>
        /// <returns></returns>
        public static bool AllowAction(this IHtmlHelper html, Actions actionEnum, string name)
        {
            return true;
            //var nodes = SystemMenu.TopNode;
            //var node = nodes.FindPageNode(name);
            //if (node == null)
            //{
            //    throw new NotSupportedException();
            //}
            //return node.IsAllow(actionEnum);
        }
    }
}