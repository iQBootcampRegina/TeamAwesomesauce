using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using PaymentServiceMessageContracts;

namespace PaymentService.Messages
{
	public class NewUnpaidOrderMessage : INewUnpaidOrderMessage
	{
		public NewUnpaidOrderMessage(Guid cartID, decimal amountDue)
		{
			this.CartID = cartID;
			this.AmountDue = amountDue;
		}

		public Guid CartID { get; private set; }
		public decimal AmountDue { get; private set; }
	}
}