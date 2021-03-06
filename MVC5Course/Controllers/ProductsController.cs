﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using MVC5Course.Models;
using MVC5Course.ViewModels;
using MVC5Course.ActionFilters;
using PagedList;


namespace MVC5Course.Controllers
{
    public class ProductsController : Controller
    {
        private FabricsEntities db = new FabricsEntities();

        // GET: Products
        public ActionResult Index(int pageNo=1)
        {
			var pageData = db.Product.OrderBy(p => p.ProductId);
			var result = pageData.ToPagedList(pageNo, 10);
            return View(result);
        }

		public ActionResult List()
		{
			var data = from p in db.Product.Take(10)
					   select new ProductListVM()
					   {
						   ProductId = p.ProductId,
						   ProductName = p.ProductName,
						   Price = p.Price,
						   Stock = p.Stock
					   };
			return View(data);
		}

        // GET: Products/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product product = db.Product.Find(id);
            if (product == null)
            {
                return HttpNotFound();
            }
			ViewBag.OrderLines = product.OrderLine.ToList().Take(10);
			return View(product);
        }

        // GET: Products/Create
		[DropDownListAttribute]
        public ActionResult Create()
        {
			return View();
        }

        // POST: Products/Create
        // 若要免於過量張貼攻擊，請啟用想要繫結的特定屬性，如需
        // 詳細資訊，請參閱 https://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
		[DropDownListAttribute]
		public ActionResult Create([Bind(Include = "ProductId,ProductName,Price,Active,Stock")] Product product)
        {
            if (ModelState.IsValid)
            {
                db.Product.Add(product);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
			return View(product);
        }

		// GET: Products/Edit/5
		[DropDownListAttribute]
		public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product product = db.Product.Find(id);
            if (product == null)
            {
                return HttpNotFound();
            }

			return View(product);
        }

        // POST: Products/Edit/5
        // 若要免於過量張貼攻擊，請啟用想要繫結的特定屬性，如需
        // 詳細資訊，請參閱 https://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
		[DropDownListAttribute]
		//public ActionResult Edit([Bind(Include = "ProductId,ProductName,Price,Active,Stock")] Product product)
		public ActionResult Edit(int id)
		{
			var product = db.Product.Find(id);
			if(TryUpdateModel(product, new string[] { "ProductId", "Price", "Active", "Stock" }))
			{
				db.SaveChanges();
				return RedirectToAction("Index");
			}
			/*
				   if (ModelState.IsValid)
			   {
				   db.Entry(product).State = EntityState.Modified;
				   db.SaveChanges();
				   return RedirectToAction("Index");
			   }
			   return View(product);
			   */
			return View(product);
		}

        // GET: Products/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product product = db.Product.Find(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            return View(product);
        }

        // POST: Products/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Product product = db.Product.Find(id);
            db.Product.Remove(product);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
