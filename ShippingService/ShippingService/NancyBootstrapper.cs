using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using CartService;
using IQ.Foundation.Messaging.AzureServiceBus;
using Nancy;
using Nancy.Bootstrapper;
using Nancy.TinyIoc;

namespace ShippingService
{
	public class NancyBootstrapper : DefaultNancyBootstrapper
	{
		protected override void ApplicationStartup(TinyIoCContainer container, IPipelines pipelines)
		{
			var servicebusBootstrapper = new DefaultAzureServiceBusBootstrapper(new CartServiceSubscriberConfiguration());

			servicebusBootstrapper.MessageHandlerRegisterer.Register<ProductAddedToCart>(CartServiceMessageHandler.HandleProductAddedToCartMessage);
			servicebusBootstrapper.MessageHandlerRegisterer.Register<ProductRemovedFromCart>(CartServiceMessageHandler.HandleProductRemovedFromCartMessage);

			servicebusBootstrapper.Subscribe();
		}
	}
}