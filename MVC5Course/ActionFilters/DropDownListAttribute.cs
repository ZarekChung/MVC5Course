using MVC5Course.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MVC5Course.ActionFilters
{
	public class DropDownListAttribute : ActionFilterAttribute
	{
		private FabricsEntities db = new FabricsEntities();
		public override void OnActionExecuted(ActionExecutedContext filterContext)
		{
			/*
			var items = new List<SelectListItem>();
			items.Add(new SelectListItem() { Value = "0", Text = "0" });
			items.Add(new SelectListItem() { Value = "1", Text = "1" });
			items.Add(new SelectListItem() { Value = "2", Text = "2" });

			ViewBag.Price = items;
			*/
			var price_list = (from p in db.Product
							  select new
							  {
								  Value = p.Price,
								  Text = p.Price
							  }).Distinct().OrderBy(p => p.Value);
			filterContext.Controller.ViewBag.Price = new SelectList(price_list, "Value", "Text");
			base.OnActionExecuted(filterContext);
		}
	}
}