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

		public void AddProduct(Guid productId)
		{
			if (!_contents.ContainsKey(productId))
				_contents.Add(productId, 1);
			else
				_contents[productId]++;
		}

		public void RemoveProduct(Guid productId)
		{
			if (!_contents.ContainsKey(productId))
				_contents.Add(productId, -1);
			else
				_contents[productId]--;
		}
	}

	/// <summary>
	/// Key = Cart Identifier
	/// Value = The contents of the Key's cart
	/// </summary>
	public static class ShippingCarts
	{
		private static Dictionary<Guid, ShippingCart> _carts = new Dictionary<Guid, ShippingCart>();
		// Thanks to Ryan Marcotte for telling us about IReadOnlyDictionary in .NET 4.5.
		public static IReadOnlyDictionary<Guid, ShippingCart> Carts { get { return _carts; } }

		public static void AddToCart(Guid cartId, Guid productId)
		{
			if (!Carts.ContainsKey(cartId))
				_carts.Add(cartId, new ShippingCart());

			_carts[cartId].AddProduct(productId);
		}

		public static void RemoveFromCart(Guid cartId, Guid productId)
		{
			if (!Carts.ContainsKey(cartId))
				_carts.Add(cartId, new ShippingCart());

			_carts[cartId].RemoveProduct(productId);
		}

		//public static void 
	}

	public static class ShippingProductCache
	{
		private static Dictionary<Guid, ShippingProduct> _products = new Dictionary<Guid, ShippingProduct>(); 
		// Thanks to Ryan Marcotte for telling us about IReadOnlyDictionary in .NET 4.5.
		public static IReadOnlyDictionary<Guid, ShippingProduct> Products { get { return _products;} }

		public static ShippingProduct ObtainProductShippingInfo(Guid productId)
		{
			if (!_products.ContainsKey(productId))
				CacheProductShippingInfo(productId);

			return _products[productId];
		}

		private static void CacheProductShippingInfo(Guid productId)
		{
			// In reality, we'd connect to the Product Library to request/response
			// the shipping info for this product. However, we're not implementing
			// that service/API in this bootcamp, so create random numbers instead.
			var pseudoRandomDecimalGenerator = new Random(DateTime.UtcNow.Millisecond);
			var dimensions = new Dimensions(height: pseudoRandomDecimalGenerator.Next(1, 50000)/100.0m,
			                                width: pseudoRandomDecimalGenerator.Next(1, 50000)/100.0m,
			                                length: pseudoRandomDecimalGenerator.Next(1, 50000)/100.0m);
			var weight = pseudoRandomDecimalGenerator.Next(1, 100000)/100.0m;
			var shippingInfo = new ShippingProduct(dimensions, weight);

			// Cache the "retrieved" shipping info for this product.
			if (!_products.ContainsKey(productId))
				_products.Add(productId, shippingInfo);
			else
				_products[productId] = shippingInfo;
		}
	}

}