using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using PaymentServiceMessageContracts;

namespace PaymentService.Messages
{
	public class PaymentCompleteModel : IPaymentCompleteModel
	{
		public Guid CartID { get; private set; }

		public PaymentCompleteModel(Guid cartID)
		{
			CartID = cartID;
		}
	}
}