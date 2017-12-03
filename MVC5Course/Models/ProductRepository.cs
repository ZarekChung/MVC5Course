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
	}

	public  interface IProductRepository : IRepository<Product>
	{

	}
}