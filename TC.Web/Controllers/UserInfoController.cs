using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using System.Threading.Tasks;
using TC.Model.Entitys;
using TC.Persistence;
using TC.Web.Utility;

namespace TC.Web.Controllers
{
    public class UserInfoController : Controller
    {

        /// <summary>
        /// 数据库连接上下文
        /// </summary>
        public readonly SqlContext db;

        public readonly IHttpContextAccessor _httpContextAccessor;

        /// <summary>
        /// 构造函数，注入Sql上下文
        /// </summary>
        /// <param name="sqlContext"></param>
        public UserInfoController(SqlContext sqlContext, IHttpContextAccessor httpContextAccessor)
        {
            this.db = sqlContext;
            this._httpContextAccessor = httpContextAccessor;
        }

        /// <summary>
        /// 系统后台用户信息
        /// </summary>
        /// <returns></returns>
        public async Task<ViewResult> Index(int? pageIndex)
        {
            var where = Searcher.Init(_httpContextAccessor)
                .Condition<UserInfoView>().ToAndPredicate();


            var pagesize = Searcher.Init(_httpContextAccessor).GetValue("pagesize").ToInt32(10);
            var orderby = Searcher.Init(_httpContextAccessor).OrderBy.NullThen("OR_ID desc");
            var model = await db.UserInfoView.Where(where).ToPageAsync(orderby, pageIndex.ToIndex(), pagesize);
            if (Request.IsAjax())
            {
                return View("ToPage", model);
            }
            return View(model);
        }
    }
}