using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PaymentServiceMessageContracts;

namespace PaymentService.Messages
{
	class SubmitPaymentMessage : ISubmitPaymentMessage
	{
		public SubmitPaymentMessage(Guid cartID, string billingInfo, decimal amount)
		{
			this.CartID = cartID;
			this.BillingInfo = billingInfo;
			this.AmountDue = amount;
		}
		
		public Guid CartID { get; private set; }
		public string BillingInfo { get; private set; }
		public decimal AmountDue { get; private set; }
	}
}
