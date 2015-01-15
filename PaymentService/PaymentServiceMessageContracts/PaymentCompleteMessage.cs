using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using PaymentServiceMessageContracts;

namespace PaymentServiceMessageContracts
{
	public class PaymentCompleteMessage
	{
		public Guid CartID { get; private set; }

		public PaymentCompleteMessage(Guid cartID)
		{
			CartID = cartID;
		}
	}
}