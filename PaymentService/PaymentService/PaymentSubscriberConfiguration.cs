using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using IQ.Foundation.Messaging.AzureServiceBus.Configuration;

namespace PaymentService
{
	public class PaymentSubscriberConfiguration : ConventionServiceBusConfiguration
	{
		public override string ConnectionString
		{
			get { return "Endpoint=sb://iqbootcamp.servicebus.windows.net/;SharedAccessKeyName=RootManageSharedAccessKey;SharedAccessKey=DYR29cbRrvBi1ADCztJQ99vlTOyPDVFAtLOgO8yWgN8="; }
		}

		public override string ServiceIdentifier
		{
			get { return "Boreal.PaymentSubscriber"; }
		}

		protected override IEnumerable<string> SubscriptionTopics
		{
			get
			{
				yield return "Boreal.PaymentPublisher";
			}
		}
	}
}