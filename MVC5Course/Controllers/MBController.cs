using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MVC5Course.Controllers
{
    public class MBController : BaseController
    {
        // GET: MB
        public ActionResult Index()
        {
			var data = repo.GetAllProductTop15();

			ViewData.Model = data;

			//ViewBag.Title = "Product list";
			ViewData["Title"] = "Product List";			
            return View();
        }
    }
}