using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using TC.Persistence;

namespace TC.Web.Controllers
{
    /// <summary>
    /// 表示公共基础控制器
    /// </summary>
    public class BaseController : Controller
    {
        /// <summary>
        /// 数据库连接上下文
        /// </summary>
        public readonly SqlContext db;

        /// <summary>
        /// 配置文件
        /// </summary>
        public readonly IConfiguration _configuration;

        /// <summary>
        /// 站点基础配置信息
        /// </summary>
        public readonly WebConfig webConfig;

        public BaseController()
        {
            this.db = (SqlContext)HttpContext.RequestServices.GetService(typeof(SqlContext));
            this._configuration = (IConfiguration)this.HttpContext.RequestServices.GetService(typeof(IConfiguration));
            var c = this._configuration.GetSection("WebConfig").Value;
        }

    }
}