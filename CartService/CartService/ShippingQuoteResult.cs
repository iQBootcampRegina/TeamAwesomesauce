using System;

namespace CartService
{
	public class ShippingQuoteResult : IShippingQuoteResult
	{
		public Guid CartId { get; set; }
		public decimal Price{ get; set; }

		public ShippingQuoteResult(Guid cartId, decimal price)
		{
			CartId = cartId;
			Price = price;
		}
	}


}