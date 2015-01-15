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
			if (UnpaidCartDataStore.GetAmount(request.CartID) != null)
			{
				PublishMessage(messagePublisher, new PaymentCompleteModel(request.CartID));
				UnpaidCartDataStore.SetOrderAsPaid(request.CartID);
				return Negotiate.WithStatusCode(HttpStatusCode.Created).WithReasonPhrase("Thank you for your payment.");
			}
			return Negotiate.WithStatusCode(HttpStatusCode.BadRequest).WithReasonPhrase("Cart does not exist");
		}

		private static void PublishMessage(IPublishMessages messagePublisher, object message)
		{
			messagePublisher.Publish(message);
		}
	}
}