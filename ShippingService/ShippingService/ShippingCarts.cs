using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ProductService;

namespace ShippingService
{
	/// <summary>
	/// Key = Product Identifier
	/// Value = Quantity of the Key's product in the Shopping Cart
	/// </summary>
	public class ShippingCart
	{
		private Dictionary<Guid, int> _contents;
		// Thanks to Ryan Marcotte for telling us about IReadOnlyDictionary in .NET 4.5.
		public IReadOnlyDictionary<Guid, int> Contents { get { return _contents; } }

		public ShippingCart()
		{
			_contents = new Dictionary<Guid, int>();
		}

		public void AddProduct(Guid productID)
		{
			if (!_contents.ContainsKey(productID))
				_contents.Add(productID, 1);
			else
				_contents[productID]++;
		}

		public void RemoveProduct(Guid productID)
		{
			if (!_contents.ContainsKey(productID))
				_contents.Add(productID, -1);
			else
				_contents[productID]--;
		}
	}

	/// <summary>
	/// Key = Cart Identifier
	/// Value = The contents of the Key's cart
	/// </summary>
	public static class ShippingCarts
	{
		private Dictionary<Guid, ShippingCart> 
		public static Dictionary<Guid, ShippingCart> Carts = new Dictionary<Guid, ShippingCart>();

		public static void AddToCart(Guid cartID, Guid productID)
		{
			if (!Carts.ContainsKey(cartID))
				Carts.Add(cartID, new ShippingCart());

			Carts[cartID].AddProduct(productID)
		}
	}
}