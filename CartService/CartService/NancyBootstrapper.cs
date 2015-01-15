using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using IQ.Foundation.Messaging.AzureServiceBus;
using Nancy;
using Nancy.Bootstrapper;
using Nancy.TinyIoc;
using PaymentServiceMessageContracts;

namespace CartService
{
	public class NancyBootstrapper :DefaultNancyBootstrapper

	{
		protected override void ApplicationStartup(TinyIoCContainer container, IPipelines pipelines)
		{
			// your customization goes here

			DefaultAzureServiceBusBootstrapper bootstrapper = new DefaultAzureServiceBusBootstrapper(new CartServiceConfiguration());
			var messagePublisher = bootstrapper.BuildMessagePublisher();

			var messageQueuePublisher = bootstrapper.BuildQueueProducer();
			container.Register(messagePublisher);
			container.Register(messageQueuePublisher);

			// Listen to Shipping info being received
			var cartModule = container.Resolve<CartModule>();
			bootstrapper.MessageHandlerRegisterer.Register<ShippingQuoteResult>(cartModule.ShippingPriceUpdateddHandler);
			// Listen to cart payments complete
			bootstrapper.MessageHandlerRegisterer.Register<PaymentCompleteMessage>(cartModule.PaymentCompletedHandler);
			bootstrapper.Subscribe();
		}

	}
}