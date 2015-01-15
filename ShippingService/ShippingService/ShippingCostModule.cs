using CartService;
using IQ.Foundation.Messaging.AzureServiceBus;
using IQ.Foundation.Messaging.MessageHandling;
using Nancy;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Nancy.ModelBinding;

namespace ShippingService
{
	

	/// <summary>
	/// Responsible for obtaining a shipping cost given customer postal code, plus
	/// the package's total size and weight.
	/// </summary>
	public class ShippingCostModule : NancyModule
	{
		
		public static string lastMessage;


		public ShippingCostModule()
		{
			Nancy.StaticConfiguration.DisableErrorTraces = false;
			Get["/cost"] = x => GetShippingCost();
			Get["/"] = x => { return lastMessage; };
		}

		public object GetShippingCost()
		{
			var request = this.Bind<ShippingQuoteParameters>();

			return Negotiate.WithModel(new ShippingQuoteResult
				{
					Currency = "CAD",
					Cost = Decimal.Round(0.025m * ((0.3m * request.TotalSize) + (0.7m * request.TotalWeight)), 2, MidpointRounding.AwayFromZero)
				}).WithStatusCode(HttpStatusCode.OK);
		}
	}

	public class ShippingQuoteParameters
	{
		public string PostalCode { get; set; }
		public decimal TotalSize { get; set; }
		public decimal TotalWeight { get; set; }
	}

	public class ShippingQuoteResult
	{
		public string Currency { get; set; }
		public decimal Cost { get; set; }
	}

	public static class CartServiceMessageHandler
	{

		public static void HandleProductAddedToCartMessage(ProductAddedToCart message)
		{
			Console.WriteLine("message recieved" + message.ToString());

			ShippingCostModule.lastMessage = "add " + message.ToString();
		}

		public static void HandleProductRemovedFromCartMessage(ProductRemovedFromCart message)
		{
			Console.WriteLine("message recieved" + message.ToString());

			ShippingCostModule.lastMessage = "remove " + message.ToString();
		}

		private static void EnsureCartExistsInLocalModel(Guid cartID)
		{
			
		}
	}
}