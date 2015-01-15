using System.Collections.Generic;
using IQ.Foundation.Messaging.AzureServiceBus.Configuration;

namespace CartService
{
	public class CartServiceConfiguration : ConventionServiceBusConfiguration
	{
		public override string ConnectionString
		{
			get { return "Endpoint=sb://iqbootcamp.servicebus.windows.net/;SharedAccessKeyName=RootManageSharedAccessKey;SharedAccessKey=DYR29cbRrvBi1ADCztJQ99vlTOyPDVFAtLOgO8yWgN8="; }
		}

		public override string ServiceIdentifier
		{
			get { return "TA.CartService"; }
		}

		protected override bool PublishesMessages
		{
			get { return true; }
		}

		protected override IEnumerable<string> SubscriptionTopics
		{
			get { yield return "TA.PaymentService"; }
		}
	}

}