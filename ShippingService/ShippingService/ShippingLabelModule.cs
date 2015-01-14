using Nancy;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ShippingService
{
	public class ShippingLabelModule : NancyModule
	{
		public ShippingLabelModule()
		{
			Get["/"] = x => GetShippingLabel();
		}

		public object GetShippingLabel()
		{
			return Guid.NewGuid();
		}
	}
}