using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using IQ.Foundation.Messaging.AzureServiceBus;
using IQ.Foundation.Messaging.AzureServiceBus.Configuration;
using Nancy;
using Nancy.Bootstrapper;
using Nancy.TinyIoc;
using PaymentService.Configurations;
using PaymentServiceMessageContracts;

namespace PaymentService
{
	public class NancyBootstrapper : DefaultNancyBootstrapper
	{
		protected override void ApplicationStartup(TinyIoCContainer container, IPipelines pipelines)
		{
			base.ApplicationStartup(container, pipelines);

			var serviceBusBootStrapper = new AzureBootstrapper(new PaymentConfiguration());

			serviceBusBootStrapper.MessageHandlerRegisterer.Register<NewCartCheckoutMessage>(HandlePaymentMessage);
			serviceBusBootStrapper.Subscribe();
		}

		private static void HandlePaymentMessage(NewCartCheckoutMessage message)
		{
			if(message != null)
				UnpaidCartDataStore.SetAmount(message.CartID, message.AmountDue);

			//Console.WriteLine(string.Format("Received new order => ID: {0} for order ID {1}", message.CartID, message.Amount));
		}
	}
}