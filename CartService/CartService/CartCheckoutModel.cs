using System;

namespace CartService
{
	internal class CartCheckoutModel
	{
		public CartCheckoutModel(Guid id, decimal amount)
		{
			CartId = id;
			CartAmount = amount;
		}

		public decimal CartAmount { get; set; }

		public Guid CartId { get; set; }
	}
}