using System;
using System.Linq;
using System.Collections.Generic;
	
namespace MVC5Course.Models
{   
	public  class ProductRepository : EFRepository<Product>, IProductRepository
	{
		public Product Find(int Id)
		{
			return this.All().FirstOrDefault(p => p.ProductId == Id);
		}

		public IQueryable<Product> GetAllProduct()
		{
			return this.All().Where(p => p.isDeleted == false);
		}

		public IQueryable<Product> GetAllProductTop15()
		{
			return this.All().Where(p => p.isDeleted == false).Take(15);
		}
	}

	public  interface IProductRepository : IRepository<Product>
	{

	}
}