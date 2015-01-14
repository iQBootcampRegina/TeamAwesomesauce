using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Nancy;
using Nancy.ModelBinding;

namespace CartService
{
	public class CartModule : NancyModule
	{
		private static List<CartModel> _carts  = new List<CartModel>();


		public CartModule()
		{
			Post["/Carts"] = x => CreateCart();
			Post["/Carts/{id}/Products"] = x =>AddProductToCart(x.id);
			Delete["/Carts/{id}/Products/{productId}"] = x => RemoveProductToCart(x.id, x.productId);

		}

		private object RemoveProductToCart(Guid id, Guid productId)
		{
			CartModel cart = GetCartById(id);
			cart.Products.Remove(cart.Products.FirstOrDefault(x => x.Equals(productId)));
			return HttpStatusCode.OK;
		}

		private CartModel GetCartById(Guid id)
		{
			return _carts.First(x => x.id == id);

		}

		private object AddProductToCart(Guid id)
		{
			var request = this.Bind<AddProductRequest>();
			var cart = GetCartById(id);
			cart.Products.Add(request.productId);
			return HttpStatusCode.OK;

		}

		private object CreateCart()
		{
			var cartModel = new CartModel() { id = new Guid(), Products = new List<Guid>() };
			_carts.Add(cartModel);
			return cartModel;
		}
	}

	internal class AddProductRequest

	{
		public Guid productId { get; set; }
	}

	public class CartModel
	{
		public Guid id { get; set; }

		public IList<Guid> Products { get; set; }
	}
}