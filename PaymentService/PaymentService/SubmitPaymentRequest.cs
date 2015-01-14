using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PaymentService
{
	public class SubmitPaymentRequest
	{
		public Guid OrderID { get; set; }

		public string Address { get; set; }
		public string City { get; set; }
		public string Province { get; set; }
		public string PostalCode { get; set; }

		public string CardholderName { get; set; }
		public string CreditCardNumber { get; set; }
		public string CCV { get; set; }
		public string Expiry { get; set; }

		public decimal Amount { get; set; }

		public bool IsValid()
		{
			return !string.IsNullOrEmpty(Address) && !string.IsNullOrEmpty(City)
				   && !string.IsNullOrEmpty(Province) && !string.IsNullOrEmpty(PostalCode)
				   && !string.IsNullOrEmpty(CardholderName) && !string.IsNullOrEmpty(CreditCardNumber)
				   && !string.IsNullOrEmpty(CCV) && !string.IsNullOrEmpty(Expiry)
				   && Amount > 0.0m;
		}
	}
}