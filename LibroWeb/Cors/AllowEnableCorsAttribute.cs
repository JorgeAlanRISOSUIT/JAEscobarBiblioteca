using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.UI;

namespace LibroWeb.Cors
{
    public class AllowEnableCorsAttribute : ActionFilterAttribute
    {

        public override void OnActionExecuted(ActionExecutedContext filterContext)
        {
            filterContext.RequestContext.HttpContext.Response.AddHeader("Access-Control-Allow-Origin", "https://localhost:44367/");
            filterContext.RequestContext.HttpContext.Response.AddHeader("Access-Control-Request-Method", "*");
            filterContext.RequestContext.HttpContext.Response.AddHeader("Access-Control-Request-Headers", "*");
        }
    }
}