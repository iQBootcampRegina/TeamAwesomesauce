using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using IQ.Foundation.Messaging.AzureServiceBus.Configuration;

namespace PaymentService.Configurations
{
	public class PaymentConfiguration : ConventionServiceBusConfiguration
	{
		public override string ConnectionString
		{
			get { return "Endpoint=sb://iqbootcamp.servicebus.windows.net/;SharedAccessKeyName=RootManageSharedAccessKey;SharedAccessKey=DYR29cbRrvBi1ADCztJQ99vlTOyPDVFAtLOgO8yWgN8="; }
		}

		public override string ServiceIdentifier
		{
			get { return "TA.PaymentService"; }
		}

		protected override bool PublishesMessages
		{
			get
			{
				return true;
			}
		}

		protected override bool ConsumesQueue
		{
			get { return true; }
		}
	}
}