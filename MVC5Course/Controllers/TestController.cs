using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MVC5Course.Models;

namespace MVC5Course.Controllers
{
    public class TestController : BaseController
    {
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

				TempData["ProductItem"] = item;
				return RedirectToAction("Index");
			}

			return View(data);
		}

		public ActionResult Details(int Id)
		{
			var data = repo.Find(Id);
			if (data == null)
			{
				return HttpNotFound();
			}
			return View(data);
		}

		public ActionResult Delete(int Id)
		{
			var item = repo.Find(Id);
			//var olRepo = RepositoryHelper.GetOrderRepository(repo.UnitOfWork);
			//olRepo.Delete(olRepo.All().First(p => p.OrderId == 1));

			//var olRepo = new OrderLineRepository();
			//olRepo.UnitOfWork = repo.UnitOfWork;
			//olRepo.Delete(olRepo.All().First(p => p.OrderId == 1));

			//item.isDeleted = true;
			repo.Delete(item);
			repo.UnitOfWork.Commit();

			return RedirectToAction("Index");
		}
		
    }
}