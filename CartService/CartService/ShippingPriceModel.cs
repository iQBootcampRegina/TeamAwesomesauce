using System;

namespace CartService
{
	public class ShippingPriceModel
	{
		public Guid CartId { get; set; }
		public decimal Price{ get; set; }

		public ShippingPriceModel(Guid cartId, decimal price)
		{
			CartId = cartId;
			Price = price;
		}
	}
}