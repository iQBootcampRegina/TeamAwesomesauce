using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PaymentService.Messages
{
	public class PaymentAmountMessage
	{
		public Guid OrderID { get; set; }
		public decimal AmountDue { get; set; }

		public PaymentAmountMessage(Guid orderID, decimal amountDue)
		{
			OrderID = orderID;
			AmountDue = amountDue;
		}
	}
}