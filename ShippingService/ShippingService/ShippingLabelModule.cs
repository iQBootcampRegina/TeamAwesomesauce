using Nancy;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Nancy.ModelBinding;

namespace ShippingService
{
	/// <summary>
	/// Responsible for obtaining a shipping label for an order.
	/// </summary>
	public class ShippingLabelModule : NancyModule
	{
		public ShippingLabelModule()
		{
			Nancy.StaticConfiguration.DisableErrorTraces = false;
			Post["/label"] = x => CreateShippingLabel();
			Get["/label((id))"] = x => GetShippingLabel();
		}

		public object CreateShippingLabel()
		{
			var request = this.Bind<NewShippingLabelParameters>();

			if (!shippingLabels.ContainsKey(request.OrderId))
			{
				shippingLabels.Add(request.OrderId, Guid.NewGuid());
			}
			else
			{
				return Negotiate.WithStatusCode(HttpStatusCode.Conflict).WithReasonPhrase("This order already has an attached shipment. Go to /label((OrderId)) to obtain the ShippingId.");
			}

			var result = new ShippingLabelResult
				{
					ShipmentId = shippingLabels[request.OrderId]
				};

			return Negotiate.WithModel(result).WithStatusCode(HttpStatusCode.Created);
		}

		public object GetShippingLabel()
		{
			var request = this.Bind<ExistingShippingLabelParameters>();

			if (!shippingLabels.ContainsKey(request.OrderId))
			{
				return Negotiate.WithStatusCode(HttpStatusCode.NotFound);
			}

			var result = new ShippingLabelResult
				{
					ShipmentId = shippingLabels[request.OrderId]
				};

			return Negotiate.WithModel(result).WithStatusCode(HttpStatusCode.OK);
		}

		private Dictionary<Guid, Guid> shippingLabels;
	}

	public class NewShippingLabelParameters
	{
		public Guid OrderId { get; set; }
		public string CustomerTitle { get; set; }
		public string CustomerName { get; set; }
		public string CustomerStreetAddress { get; set; }
		public string CustomerCity { get; set; }
		public string CustomerRegion { get; set; }
		public string CustomerPostalCode { get; set; }
		public string CustomerCountry { get; set; }
	}

	public class ExistingShippingLabelParameters
	{
		public Guid OrderId { get; set; }
	}

	public class ShippingLabelResult
	{
		public Guid ShipmentId { get; set; }
	}
}