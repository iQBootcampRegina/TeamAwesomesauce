﻿using System;
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

			Post["/payment/submit"] = x => SubmitPayment();
			Get["/payment({id})"] = x => GetPayment();
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
}