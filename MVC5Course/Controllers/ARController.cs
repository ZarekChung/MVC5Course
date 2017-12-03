using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MVC5Course.Controllers
{
    public class ARController : BaseController
    {
        // GET: AR
        public ActionResult Index()
        {
            return View();
        }
		public ActionResult PartialViewTest()
		{
			return PartialView("Index");
		}

		public ActionResult ContentTest_Better()
		{
			return PartialView("JsAlertRedirect", "succeeded !!");
		}
		public ActionResult FileTest(string dl)
		{
			if(String.IsNullOrEmpty(dl))
				return File(Server.MapPath("~/App_Data/Koala.jpg"), "image/jpeg");
			else
				return File(Server.MapPath("~/App_Data/Koala.jpg"), "image/jpeg", "mypic.jpg");
		}

		public ActionResult JsonTest()
		{
			var data = from p in repo.GetAllProductTop15()
					   select new
					   {
						   p.ProductId,
						   p.ProductName,
						   p.Price
					   };
			return Json(data,JsonRequestBehavior.AllowGet);//加AllowGet表示可允許get hit
		}

    }
}