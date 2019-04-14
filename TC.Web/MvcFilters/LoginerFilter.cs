using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Linq;
using System.Threading.Tasks;
using TC.Web.Utility;

namespace TC.Web.MvcFilters
{
    public class LoginerFilter : Attribute, IAuthorizationFilter
    {

        public void OnAuthorization(AuthorizationFilterContext context)
        {
            var userToken = context.HttpContext.Request.Cookies[WebCache.userCookieKey];
            var user = WebCache.userCache.Get(userToken);

            var s = context.HttpContext.User;

        }
    }
}
