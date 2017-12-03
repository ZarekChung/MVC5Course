using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MVC5Course.Models;

namespace MVC5Course.Controllers
{
    public class TestController : Controller
    {
		FabricsEntities db = new FabricsEntities();


        // GET: Test
        public ActionResult Index()
        {
			var repo = new ProductRepository();
			repo.UnitOfWork = new EFUnitOfWork();
			
			var data = repo.All().Where(p=>p.isDeleted==false);

            return View(data.Take(10));
        }

		public ActionResult Create()
		{

			return View();
		}

		[HttpPost]
		//[ValidateAntiForgeryToken]
		public ActionResult Create(Product data)
		{
			if (ModelState.IsValid)
			{
				db.Product.Add(data);
				db.SaveChanges();
				return RedirectToAction("Index");
			}

			return View(data);
		}
		public ActionResult Edit(int Id)
		{
			var item = db.Product.Find(Id);

			return View(item);
		}

		[HttpPost]
		//[ValidateAntiForgeryToken]
		public ActionResult Edit(Product data)
		{
			if (ModelState.IsValid)
			{
				var item = db.Product.Find(data.ProductId);
				item.ProductName = data.ProductName;
				item.Price = data.Price;
				item.Stock = data.Stock;
				item.OrderLine = data.OrderLine;
				db.SaveChanges();
				return RedirectToAction("Index");
			}

			return View(data);
		}

		public ActionResult Details(int Id)
		{
			var data = db.Product.Find(Id);
			return View(data);
		}

		public ActionResult Delete(int Id)
		{
		
			var item = db.Product.Find(Id);
			//db.OrderLine.RemoveRange(item.OrderLine.ToList());
			//db.Product.Remove(item);
			item.isDeleted = true;
			db.SaveChanges();

			return RedirectToAction("Index");
		}
		
    }
}