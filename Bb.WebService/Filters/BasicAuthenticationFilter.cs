using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Bb.WebService.Filters
{
    public class BasicAuthenticationFilter : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            var req = filterContext.HttpContext.Request;
            var auth = req.Headers["Authorization"];
            if (!String.IsNullOrEmpty(auth))
            {
                var credential = System.Text.ASCIIEncoding.ASCII.GetString(Convert.FromBase64String(auth.Substring(6))).Split(':');
                // For now we hardcoded the username and password first
                // The better way is to retrieve the credentials from a database or other storage
                if (credential[0] == "BbWsUserName" && credential[1] == "BbWsP@ssword!") return;
            }
            filterContext.HttpContext.Response.AddHeader("WWW-Authenticate", "Basic realm=\"BbWebService\"");
            filterContext.Result = new HttpUnauthorizedResult();
        }
    }
}