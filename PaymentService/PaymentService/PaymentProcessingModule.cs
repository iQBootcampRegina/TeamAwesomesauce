using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using IQ.Foundation.Messaging;
using IQ.Foundation.Messaging.AzureServiceBus;
using Nancy;
using Nancy.ModelBinding;
using PaymentService.Configurations;
using PaymentService.Messages;

namespace PaymentService
{
	public class PaymentModule : NancyModule
	{
		IPublishMessages messagePublisher;

		public PaymentModule()
		{
			Post["/payment/submit"] = x => SubmitPayment();

			DefaultAzureServiceBusBootstrapper bootstrapper = new DefaultAzureServiceBusBootstrapper(new PaymentConfiguration());
			messagePublisher = bootstrapper.BuildMessagePublisher();
		}


		public object SubmitPayment()
		{
			var request = this.Bind<SubmitPaymentMessage>();
			if (UnpaidCartDataStore.UnpaidCarts.ContainsKey(request.CartID))
			{
				PublishMessage(messagePublisher, new PaymentCompleteModel(request.CartID));
				return Negotiate.WithStatusCode(HttpStatusCode.Created).WithReasonPhrase("Thank you for your payment.");
			}
			return Negotiate.WithStatusCode(HttpStatusCode.BadRequest).WithReasonPhrase("Cart does not exist");
			//					.WithModel(new PaymentResponse() { OrderID = request.OrderID, PaymentConfirmationNumber = _orderIDToPaymentIDLookup[request.OrderID] });

			//if (result)
			//{
			//	var paymentID = Guid.NewGuid();
			//	_orderIDToPaymentIDLookup.Add(request.OrderID, paymentID);

			//	return Negotiate.WithStatusCode(HttpStatusCode.Created)
			//					.WithModel(new PaymentResponse() { OrderID = request.OrderID, PaymentConfirmationNumber = paymentID });
			//}


			//return
			//	Negotiate.WithStatusCode(HttpStatusCode.BadRequest)
			//			 .WithModel(new PaymentResponse() { OrderID = request.OrderID, PaymentConfirmationNumber = null})
			//			 .WithReasonPhrase("Invalid information provided.");

		}

		private static void PublishMessage(IPublishMessages messagePublisher, object message)
		{
			messagePublisher.Publish(message);
		}
	}
}