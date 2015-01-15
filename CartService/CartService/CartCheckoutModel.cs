using System;
using PaymentServiceMessageContracts;

namespace CartService
{
	public class CartCheckoutModel
	{
		public CartCheckoutModel(Guid id, decimal amountDue)
		{
			CartID = id;
			AmountDue = amountDue;
		}

		public Guid CartID { get; private set; }
		public decimal AmountDue { get; set; }

	}
}