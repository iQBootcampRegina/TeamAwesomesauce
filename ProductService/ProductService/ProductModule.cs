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

	public class ProductModel
	{
		public ProductModel(Guid id, string name, int stock, Dimensions dimensions, decimal price, decimal weight)
		{
			ID = id;
			Name = name;
			Stock = stock;
			Dimensions = dimensions;
			Price = price;
			Weight = weight;
		}

		public Guid ID { get; set; }
		public string Name { get; set; }
		public int Stock { get; set; }
		public decimal Price { get; set; }
		public Dimensions Dimensions { get; set; }
		public decimal Weight { get; set; }
	}

	public class Dimensions
	{
		public Dimensions(decimal height, decimal width, decimal length)
		{
			Height = height;
			Width = width;
			Length = length;
		}

		public decimal Height { get; set; }
		public decimal Width { get; set; }
		public decimal Length { get; set; }

	}
}