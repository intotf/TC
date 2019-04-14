using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using TC.Web.MvcFilters;

namespace TC.Web.Controllers
{
    [LoginerFilter]
    public class SystemController : Controller
    {
        public IActionResult Center()
        {
            return View();
        }
    }
}