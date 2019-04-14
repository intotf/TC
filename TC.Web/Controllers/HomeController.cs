using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Infrastructure.Utility;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using TC.Model.Entitys;
using TC.Persistence;
using TC.Web.Models;
using TC.Web.Utility;

namespace TC.Web.Controllers
{
    public class HomeController : Controller
    {
        /// <summary>
        /// 数据库连接上下文
        /// </summary>
        public readonly SqlContext db;

        /// <summary>
        /// 构造函数，注入Sql上下文
        /// </summary>
        /// <param name="sqlContext"></param>
        public HomeController(SqlContext sqlContext)
        {
            this.db = sqlContext;
        }

        /// <summary>
        /// 登录首页
        /// </summary>
        /// <returns></returns>
        public IActionResult Index()
        {
            var d = db.UserInfo.FirstOrDefault();
            return View();
        }

        /// <summary>
        /// 登录验证
        /// </summary>
        /// <param name="account">账号</param>
        /// <param name="password">密码</param>
        /// <param name="code">验证码</param>
        /// <param name="refer">来源页</param>
        /// <returns></returns>
        [HttpPost]
        public async Task<JsonResult> Login(string account, string password, string code, string refer)
        {
            var checkResult = this.CheckLoginParamters(code, password, code);
            if (checkResult != null)
            {
                return checkResult;
            }

            var passwordMd5 = Encryption.GetMD5(password);
            var userInfo = await db.UserInfo.Where(item => item.Account == account && item.Password == passwordMd5).AsNoTracking().FirstOrDefaultAsync();

            if (userInfo == null)
            {
                return Json(new { state = false, value = "账号或密码不正确!" });
            }

            if (userInfo.Enable == false)
            {
                return Json(new { state = false, value = "该帐号已被禁用,请与管理员联系!" });
            }

            var userToken = Guid16.NewGuid().ToString();
            WebCache.userCache.AddOrUpdate(userToken, userInfo);
            Response.Cookies.Append(WebCache.userCookieKey, userToken, new CookieOptions
            {
                Expires = DateTimeOffset.Now.AddDays(1)
            });
            var redirect = "/system/center";
            if (string.IsNullOrEmpty(refer) == false)
            {
                var referUrl = new Uri(refer);
                if (referUrl.Host == Request.Host.Host)
                {
                    redirect = refer;
                }
            }
            return Json(new { state = true, value = redirect });
        }


        /// <summary>
        /// 检测登录
        /// </summary>
        /// <param name="account">账号</param>
        /// <param name="password">密码</param>
        /// <param name="code">验证码</param>
        /// <returns></returns>
        private JsonResult CheckLoginParamters(string account, string password, string code)
        {
            if (account.IsNullOrEmpty())
            {
                return Json(new { state = false, value = "账号不能为空!" });
            }

            if (password.IsNullOrEmpty())
            {
                return Json(new { state = false, value = "密码不能为空!" });
            }

            if (code.IsNullOrEmpty())
            {
                return Json(new { state = false, value = "验证码不能为空!" });
            }

            if (!this.VerifyUserInputCode(code))
            {
                return Json(new { state = false, value = "验证码过期或不正确,请刷新验证码!" });
            }

            // 删除验证码
            //UserSession.Remove(codeCookie.Value);
            //codeCookie.Expires = DateTime.Today.AddYears(-1);
            //Response.AppendCookie(codeCookie);
            return null;
        }


        /// <summary>
        /// 错误页面
        /// </summary>
        /// <returns></returns>
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        /// <summary>
        /// 获取验证码
        /// </summary>
        /// <returns></returns>

        public IActionResult ValidateCode()
        {
            var _vierificationCodeServices = new VerificationCode();
            string code = "";
            System.IO.MemoryStream ms = _vierificationCodeServices.Create(out code);
            code = code.ToLower();//验证码不分大小写  
            Response.Body.Dispose();
            var token = Guid16.NewGuid().ToString();
            WebCache.codeCache.AddOrUpdate(token, code);
            Response.Cookies.Append(WebCache.codeCookieKey, token, new CookieOptions
            {
                Expires = DateTimeOffset.Now.AddMinutes(1)
            });
            return File(ms.ToArray(), @"image/png");
        }

        /// <summary>
        /// 验证用户提交的验证码
        /// </summary>
        /// <param name="userVerCode"></param>
        /// <returns></returns>
        public bool VerifyUserInputCode(string code)
        {
            var token = Request.Cookies[WebCache.codeCookieKey];
            var clinesCode = WebCache.codeCache.Get(token);
            WebCache.codeCache.Delete(token);
            if (code.ToLower().Equals(clinesCode, StringComparison.Ordinal))
            {
                return true;
            }
            return false;
        }
    }
}
