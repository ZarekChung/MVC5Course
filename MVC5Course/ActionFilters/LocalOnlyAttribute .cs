using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MVC5Course.ActionFilters
{
	public class LocalOnlyAttribute : ActionFilterAttribute
	{
		public override void OnActionExecuted(ActionExecutedContext filterContext)
		{
			if (!filterContext.RequestContext.HttpContext.Request.IsLocal)
			{
				filterContext.Result = new RedirectResult("/");
			}
			base.OnActionExecuted(filterContext);
		}
	}
}