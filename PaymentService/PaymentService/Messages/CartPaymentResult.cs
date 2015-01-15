using PaymentServiceMessageContracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PaymentService.Messages
{
	public class CartPaymentResult : ICartPaymentResult
	{
		public CartPaymentResult(Guid cartID, bool paymentSuccessful)
		{
			this.CartID = cartID;
			this.PaymentSuccessful = paymentSuccessful;
		}

		public Guid CartID { get; private set; }
		public bool PaymentSuccessful { get; private set; }
	}
}