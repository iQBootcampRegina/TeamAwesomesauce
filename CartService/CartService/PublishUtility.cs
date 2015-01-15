using System;
using IQ.Foundation.Messaging;

namespace CartService
{
	public static class PublishUtility
	{
		public static void PublishMessage(IPublishMessages messagePublisher, object message)
		{
			Console.WriteLine(message.ToString());
			messagePublisher.Publish(message);
		}

		public static void PublishMessage(IEnqueueMessages messageQueuePublisher, object message)
		{
			Console.WriteLine();
			Console.WriteLine("**********");
			Console.WriteLine("Producing message '{0}' . . .", message);

			messageQueuePublisher.Enqueue("IQ.SampleQueueConsumer", message);

			Console.WriteLine("DONE!!");
			Console.WriteLine("**********");
			Console.WriteLine();
		}
	}
}