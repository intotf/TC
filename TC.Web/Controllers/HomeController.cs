using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TC.Model.Entitys;
using TC.Persistence;
using TC.Web.Models;

namespace TC.Web.Controllers
{
    public class HomeController : Controller
    {
        private SqlContext db;

        public HomeController(SqlContext context)
        {
            this.db = context;
        }

        public async Task<IActionResult> Index()
        {
            var model = new UserInfo();
            //if (!this.TryValidateModel(model))
            //{
            //    return Json(new { state = false, value = this.ModelState });
            //}

            // var fist = await db.UserInfo.FirstOrDefaultAsync();
            return View(model);
        }

        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
