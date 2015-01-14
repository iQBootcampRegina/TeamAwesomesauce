using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Nancy;

namespace ProductService
{
	public class ProductModule : NancyModule
	{
		public ProductModule()
		{
			Get["/Products"] = x => GetProducts();
			Get["/Products/{id}"] = x => GetProductById(x.id);
		}

		private object GetProductById(Guid id)
		{
			//todo return from repo
			return new ProductModel(id, "Your Product", 2, new Dimensions(3, 2, 1), 21.0m, 8);
		}

		private object GetProducts()
		{
			return new[]
				{
					new ProductModel(new Guid("F5025328-CCA1-4A95-8F76-0268BAC45FD5"), "Product 1", 1, new Dimensions(1,2,3), 11.11m, 12),
					new ProductModel(new Guid("2D6F192E-33E9-401C-826C-DAEF5598AC11"), "Product 2", 1, new Dimensions(4,2,3), 7.11m, 8), 
					new ProductModel(new Guid("E755E52E-362C-498F-ACA2-13EF07A1C950"), "Product 3", 1, new Dimensions(15,2,3), 61.11m, 5), 
			};
		}
	}
}