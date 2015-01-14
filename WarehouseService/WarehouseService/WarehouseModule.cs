using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Nancy;
using Nancy.ModelBinding;

namespace WarehouseService
{
	public class WarehouseModule : NancyModule
	{
		private Random RNG;
		public WarehouseModule()
		{
			RNG = new Random();
			
			Nancy.StaticConfiguration.DisableErrorTraces = false;

			Get["/Product/{id}/Location"] = x => GetLocationForProduct(x.id);
		}

		private object GetLocationForProduct(Guid productID)
		{
			return new
				{
					Row = RNG.Next(1000),
					Shelf = RNG.Next(1000),
				};

		}
	}
}