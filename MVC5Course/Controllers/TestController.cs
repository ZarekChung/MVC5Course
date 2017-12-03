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
		ProductRepository repo = RepositoryHelper.GetProductRepository();

        // GET: Test
        public ActionResult Index()
        {
			var data = repo.GetAllProductTop15();

			return View(data);
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
				repo.Add(data);
				repo.UnitOfWork.Commit();
				return RedirectToAction("Index");
			}

			return View(data);
		}
		public ActionResult Edit(int Id)
		{
			var item = repo.Find(Id);

			return View(item);
		}

		[HttpPost]
		//[ValidateAntiForgeryToken]
		public ActionResult Edit(Product data)
		{
			if (ModelState.IsValid)
			{
				var item = repo.Find(data.ProductId);
				item.ProductName = data.ProductName;
				item.Price = data.Price;
				item.Stock = data.Stock;
				item.OrderLine = data.OrderLine;
				repo.UnitOfWork.Commit();
				return RedirectToAction("Index");
			}

			return View(data);
		}

		public ActionResult Details(int Id)
		{
			var data = repo.Find(Id);
			return View(data);
		}

		public ActionResult Delete(int Id)
		{
		
			var item = repo.Find(Id);
			
			item.isDeleted = true;
			repo.UnitOfWork.Commit();

			return RedirectToAction("Index");
		}
		
    }
}