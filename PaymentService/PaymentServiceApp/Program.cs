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
		private static readonly Dictionary<Guid, decimal> _unpaidOrderAmount = new Dictionary<Guid, decimal>();
		public IReadOnlyDictionary<Guid, decimal> UnpaidOrder

		static void Main(string[] args)
		{
			var serviceBusBootStrapper = new DefaultAzureServiceBusBootstrapper(new MachineScopedServiceBusConfiguration(new PaymentSubscriberConfiguration()));

			serviceBusBootStrapper.MessageHandlerRegisterer.Register<PaymentAmountMessage>(HandlePaymentMessage);
			serviceBusBootStrapper.Subscribe();

			Console.ReadLine();
		}

		private static void HandlePaymentMessage(PaymentAmountMessage message)
		{
			_unpaidOrderAmount.Add(message.OrderID, message.AmountDue);
			Console.WriteLine(string.Format("Received new order => ID: {0} for order ID {1}", message.OrderID, message.AmountDue));
		}
	}
}
