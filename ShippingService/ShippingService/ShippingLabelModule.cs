using Nancy;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

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

			Get["/"] = x => GetShippingLabel();
		}

		public object GetShippingLabel()
		{
			return Guid.NewGuid();
		}
	}
}