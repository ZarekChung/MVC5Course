using System;
using System.Linq;
using System.Collections.Generic;
	
namespace MVC5Course.Models
{   
	public  class ProductRepository : EFRepository<Product>, IProductRepository
	{
		public override IQueryable<Product> All()
		{
			return base.All().Where(p => p.isDeleted == false); 
		}

		public Product Find(int Id)
		{
			return this.All().FirstOrDefault(p => p.ProductId == Id);
		}

		public IQueryable<Product> GetAllProduct()
		{
			return this.All();
		}

		public IQueryable<Product> GetAllProductTop15()
		{
			return this.All().Take(15);
		}

		public override void Delete(Product entity)
		{
			entity.isDeleted = true;
		}
	}

	public  interface IProductRepository : IRepository<Product>
	{

	}
}