using Nancy;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ShippingService
{
	public class ShippingCostModule : NancyModule
	{
		public ShippingCostModule()
		{
			Get["/"] = x => GetShippingCost(x.PostalCode, x.TotalSize, x.TotalWeight);
		}

		public object GetShippingCost(string postalCode, decimal totalSize, decimal totalWeight)
		{
			return 10.0m;
		}
	}
}