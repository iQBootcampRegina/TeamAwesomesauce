using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using IQ.Foundation.Messaging.AzureServiceBus.Configuration;

namespace ShippingService
{
	public class CartServiceSubscriberConfiguration : ConventionServiceBusConfiguration
	{
		public override string ConnectionString
		{
			get { return "Endpoint=sb://iqbootcamp.servicebus.windows.net/;SharedAccessKeyName=RootManageSharedAccessKey;SharedAccessKey=DYR29cbRrvBi1ADCztJQ99vlTOyPDVFAtLOgO8yWgN8="; }
		}

		public override string ServiceIdentifier
		{
			get { return "TA.ShippingService"; }
		}

		protected override IEnumerable<string> SubscriptionTopics
		{
			get { yield return "TA.CartService"; }
		}
	}
}