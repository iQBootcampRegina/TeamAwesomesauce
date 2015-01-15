using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using IQ.Foundation.Messaging.AzureServiceBus.Configuration;

namespace PaymentService.Configurations
{
	public class PaymentSubscriberConfiguration : ConventionServiceBusConfiguration
	{
		public override string ConnectionString
		{
			get { return "Endpoint=sb://iqbootcamp.servicebus.windows.net/;SharedAccessKeyName=RootManageSharedAccessKey;SharedAccessKey=DYR29cbRrvBi1ADCztJQ99vlTOyPDVFAtLOgO8yWgN8="; }
		}

		public override string ServiceIdentifier
		{
			get { throw new NotImplementedException(); } // }return "Boreal.PaymentSubscriber"; }
		}

		protected override IEnumerable<string> SubscriptionTopics
		{
			get
			{
				throw new NotImplementedException();
				//yield return "Boreal.PaymentPublisher";
			}
		}
	}
}