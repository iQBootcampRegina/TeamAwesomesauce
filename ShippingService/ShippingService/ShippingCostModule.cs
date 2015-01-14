using Nancy;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ShippingService
{
	/// <summary>
	/// Responsible for obtaining a shipping cost given customer postal code, plus
	/// the package's total size and weight.
	/// </summary>
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