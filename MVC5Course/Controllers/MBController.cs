using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MVC5Course.Controllers
{
	public class MbViewModel
	{
		public int ProductId { get; set; }
		public Nullable<decimal> Price { get; set; }
		public Nullable<bool> Active { get; set; }
		public Nullable<decimal> Stock { get; set; }
	}
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

		[HttpPost]
		//[ValidateAntiForgeryToken]
		public ActionResult Index(MbViewModel[] batch)
		{
			if (ModelState.IsValid)
			{
				foreach (var item in batch)
				{
					try
					{
					var one = repo.Find(item.ProductId);
					one.Price = item.Price;
					one.Stock = item.Stock;
					one.Active = item.Active;
					
						repo.UnitOfWork.Commit();
					}
					catch (DbEntityValidationException ex)
					{
						throw;
					}
					
				}
				return RedirectToAction("Index");
			}

			var data = repo.GetAllProductTop15();
			ViewData.Model = data;
			ViewBag.PageTitle = "商品清單";
			return View();
		}
    }
}