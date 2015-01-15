using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ProductService;

namespace ShippingService
{
	public class ShippingProduct
	{
		public ShippingProduct(Dimensions dimensions, decimal weight)
		{
			Dimensions = dimensions;
			Weight = weight;
		}

		public Dimensions Dimensions { get; set; }
		public Decimal Weight { get; set; }
	}
}