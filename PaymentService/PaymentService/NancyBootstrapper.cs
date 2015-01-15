using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using IQ.Foundation.Messaging.AzureServiceBus;
using IQ.Foundation.Messaging.AzureServiceBus.Configuration;
using Nancy;
using PaymentService.Configurations;
using PaymentService.Messages;

namespace PaymentService
{
	public class NancyBootstrapper : DefaultNancyBootstrapper
	{
		private static readonly Dictionary<Guid, decimal> _unpaidOrderAmounts = new Dictionary<Guid, decimal>();
		public IReadOnlyDictionary<Guid, decimal> UnpaidOrderAmounts { get { return _unpaidOrderAmounts; } }

		protected override void ApplicationStartup(Nancy.TinyIoc.TinyIoCContainer container, Nancy.Bootstrapper.IPipelines pipelines)
		{
			base.ApplicationStartup(container, pipelines);

			var serviceBusBootStrapper = new DefaultAzureServiceBusBootstrapper(new MachineScopedServiceBusConfiguration(new PaymentSubscriberConfiguration()));

			serviceBusBootStrapper.MessageHandlerRegisterer.Register<NewUnpaidOrderMessage>(HandlePaymentMessage);
			serviceBusBootStrapper.Subscribe();
		}

		private static void HandlePaymentMessage(NewUnpaidOrderMessage message)
		{
			if (_unpaidOrderAmounts.ContainsKey(message.CartID))
				_unpaidOrderAmounts[message.CartID] = message.AmountDue;
			else
				_unpaidOrderAmounts.Add(message.CartID, message.AmountDue);

			//Console.WriteLine(string.Format("Received new order => ID: {0} for order ID {1}", message.CartID, message.Amount));
		}
	}
}