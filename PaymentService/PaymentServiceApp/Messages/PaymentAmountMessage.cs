using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using PaymentServiceMessageContracts;

namespace PaymentService.Messages
{
	public class PaymentAmountMessage : IPaymentAmountMessage
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