using IQ.Foundation.Messaging.AzureServiceBus;
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
		public static void Main(string[] args)
		{
			var servicebusBootstrapper = new DefaultAzureServiceBusBootstrapper(new CartServiceSubscriberConfiguration());
		}


		public ShippingCostModule()
		{
			Nancy.StaticConfiguration.DisableErrorTraces = false;
			Get["/cost"] = x => GetShippingCost();
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
}