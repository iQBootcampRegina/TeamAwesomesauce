using System;

namespace ProductService
{
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
}