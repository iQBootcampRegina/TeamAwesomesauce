using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using IQ.Foundation.Messaging.AzureServiceBus;
using IQ.Foundation.Messaging.AzureServiceBus.Configuration;

namespace PaymentService
{
	public class AzureBootstrapper : DefaultAzureServiceBusBootstrapper
	{
		public AzureBootstrapper(IProvideServiceBusConfiguration serviceBusConfigurationProvider) : base(serviceBusConfigurationProvider)
		{
		}

		protected override void CreateQueues(IEnumerable<QueueConsumerConfiguration> consumerConfigurations)
		{
			var newConfigs = consumerConfigurations.Select(queueConsumerConfiguration => new QueueConsumerConfiguration(queueConsumerConfiguration.QueueName + ".Queue"));

			base.CreateQueues(newConfigs);
		}

		protected override void RegisterQueueConsumers(IEnumerable<QueueConsumerConfiguration> consumerConfigurations)
		{
			var newConfigs = consumerConfigurations.Select(queueConsumerConfiguration => new QueueConsumerConfiguration(queueConsumerConfiguration.QueueName + ".Queue"));

			base.RegisterQueueConsumers(newConfigs);
		}

	}
}