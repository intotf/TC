using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TC.Web.Utility
{
    public class Test
    {
        private IHttpContextAccessor httpContextAccessor;

        public Test(IHttpContextAccessor _httpContextAccessor)
        {
            this.httpContextAccessor = _httpContextAccessor;
        }

        public string Show()
        {
            return httpContextAccessor.HttpContext.Request.Host.Host;
        }
    }
}
