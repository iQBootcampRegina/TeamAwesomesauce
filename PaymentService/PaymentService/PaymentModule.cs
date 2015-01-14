using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Nancy;
using Nancy.ModelBinding;

namespace PaymentService
{
	public class PaymentModule : NancyModule
	{
		private readonly Dictionary<Guid, Guid> _orderIDToPaymentIDLookup;

		public PaymentModule()
		{
			_orderIDToPaymentIDLookup = new Dictionary<Guid, Guid>();

			Post["/"] = x => SubmitPayment();
			Get["/"] = x => GetPayment();
		}

		public object GetPayment()
		{
			var request = this.Bind<GetPaymentRequest>();
			if (!_orderIDToPaymentIDLookup.ContainsKey(request.OrderID))
				return
					Negotiate.WithStatusCode(HttpStatusCode.NotFound)
					         .WithModel(new PaymentResponse() {OrderID = request.OrderID, PaymentConfirmationNumber = null});

			return
				Negotiate.WithStatusCode(HttpStatusCode.OK)
				         .WithModel(new PaymentResponse()
					         {
						         OrderID = request.OrderID,
						         PaymentConfirmationNumber = _orderIDToPaymentIDLookup[request.OrderID]
					         });
		}

		public object SubmitPayment()
		{
			var request = this.Bind<SubmitPaymentRequest>();

			var result = request.IsValid();
			if (result)
			{
				var paymentID = Guid.NewGuid();
				_orderIDToPaymentIDLookup.Add(request.OrderID, paymentID);

				return Negotiate.WithStatusCode(HttpStatusCode.Created)
								.WithModel(new PaymentResponse() { OrderID = request.OrderID, PaymentConfirmationNumber = paymentID });
			}
				
			
			return
				Negotiate.WithStatusCode(HttpStatusCode.BadRequest)
				         .WithModel(new PaymentResponse() { OrderID = request.OrderID, PaymentConfirmationNumber = null})
						 .WithReasonPhrase("Invalid information provided.");
		}
	}

	public class GetPaymentRequest
	{
		public Guid OrderID { get; set; }
	}

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

	public class PaymentResponse
	{
		public Guid OrderID { get; set; }
		public Guid? PaymentConfirmationNumber { get; set; }
	}
}