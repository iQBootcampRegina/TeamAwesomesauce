using System;
using System.Collections.Generic;

namespace CartService
{
	public class CartModel
	{
		public Guid id { get; set; }

		public IList<Guid> Products { get; set; }
	}
}