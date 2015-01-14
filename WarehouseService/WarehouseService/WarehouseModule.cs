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
		public WarehouseModule()
		{
			Nancy.StaticConfiguration.DisableErrorTraces = false;

			Get["/Product/{id}/Location"] = x => GetLocationForProduct(x.id);
		}

		private object GetLocationForProduct(Guid productID)
		{
			return new
				{
					Row = new Random().Next(1000),
					Shelf = new Random().Next(1000),
				};

		}
	}
}