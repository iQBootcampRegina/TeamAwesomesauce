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
		private static readonly Dictionary<Guid, Guid> _orderIDToPaymentIDLookup = new Dictionary<Guid, Guid>();

		public PaymentModule()
		{
			Post["/payment/submit"] = x => SubmitPayment();
			Get["/payment({id})"] = x => GetPayment(x.id);
		}

		public object GetPayment(Guid orderId)
		{
			if (!_orderIDToPaymentIDLookup.ContainsKey(orderId))
				return
					Negotiate.WithStatusCode(HttpStatusCode.NotFound)
							 .WithModel(new PaymentResponse() { OrderID = orderId, PaymentConfirmationNumber = null });

			return
				Negotiate.WithStatusCode(HttpStatusCode.OK)
				         .WithModel(new PaymentResponse()
					         {
								 OrderID = orderId,
								 PaymentConfirmationNumber = _orderIDToPaymentIDLookup[orderId]
					         });
		}

		public object SubmitPayment()
		{
			var request = this.Bind<SubmitPaymentRequest>();
			if (_orderIDToPaymentIDLookup.ContainsKey(request.OrderID))
				return Negotiate.WithStatusCode(HttpStatusCode.Conflict)
								.WithModel(new PaymentResponse() { OrderID = request.OrderID, PaymentConfirmationNumber = _orderIDToPaymentIDLookup[request.OrderID] });

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
}