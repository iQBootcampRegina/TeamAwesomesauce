using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IQ.Foundation.Messaging.AzureServiceBus;
using IQ.Foundation.Messaging.AzureServiceBus.Configuration;
using IQ.Foundation.Messaging.MessageHandling;
using PaymentService.Configurations;
using PaymentService.Messages;

namespace PaymentService
{
	class Program
	{
		private static readonly Dictionary<Guid, decimal> _unpaidOrderAmounts = new Dictionary<Guid, decimal>();
		public IReadOnlyDictionary<Guid, decimal> UnpaidOrderAmounts { get { return _unpaidOrderAmounts; } }

		static void Main(string[] args)
		{
			var serviceBusBootStrapper = new DefaultAzureServiceBusBootstrapper(new MachineScopedServiceBusConfiguration(new PaymentSubscriberConfiguration()));

			serviceBusBootStrapper.MessageHandlerRegisterer.Register<NewUnpaidOrderMessage>(HandlePaymentMessage);
			serviceBusBootStrapper.Subscribe();

			Console.ReadLine();
		}

		private static void HandlePaymentMessage(NewUnpaidOrderMessage message)
		{
			if (_unpaidOrderAmounts.ContainsKey(message.CartID))
				_unpaidOrderAmounts[message.CartID] = message.AmountDue;
			else
				_unpaidOrderAmounts.Add(message.CartID, message.AmountDue);

			Console.WriteLine(string.Format("Received new order => ID: {0} for order ID {1}", message.OrderID, message.AmountDue));
		}
	}
}
