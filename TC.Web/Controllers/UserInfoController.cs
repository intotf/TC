using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using TC.Model.Entitys;
using TC.Persistence;
using TC.Web.Utility;
using TC.Web.Utility.Page;

namespace TC.Web.Controllers
{
    public class UserInfoController : Controller
    {

        /// <summary>
        /// 数据库连接上下文
        /// </summary>
        public readonly SqlContext db;

        /// <summary>
        /// 构造函数，注入Sql上下文
        /// </summary>
        /// <param name="sqlContext"></param>
        public UserInfoController(SqlContext sqlContext)
        {
            this.db = sqlContext;
        }

        /// <summary>
        /// 系统后台用户信息
        /// </summary>
        /// <returns></returns>
        public async Task<ViewResult> Index(int? pageIndex)
        {
            var searcher = new Searcher(this.HttpContext);

            var where = searcher
              .True<UserInfoView>()
              .And(item => item.OR_Name)
              .And(item => item.Name)
              .And(item => item.Account)
              .And(item => item.Mobile)
              .ToExpression();

            var pagesize = searcher.GetValue("pagesize").ToInt32(10);
            var orderby = searcher.OrderBy.NullThen("OR_ID desc");
            var model = await db.UserInfoView.Where(where).ToPageAsync(orderby, pageIndex.ToIndex(), pagesize);
            if (Request.IsAjax())
            {
                return View("ToPage", model);
            }
            return View(model);
        }
    }
}