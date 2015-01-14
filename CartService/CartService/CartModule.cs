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

		//Nothing wrong with this.
		private static readonly List<CartModel> _carts = new List<CartModel>();


		public CartModule()
		{
			Post["/Carts"] = x => CreateCart();
			Post["/Carts/{id}/Products"] = x => AddProductToCart(x.id);
			Delete["/Carts/{id}/Products/{productId}"] = x => RemoveProductToCart(x.id, x.productId);

		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="id"></param>
		/// <param name="productId"></param>
		/// <returns></returns>
		private object RemoveProductToCart(Guid id, Guid productId)
		{
			CartModel cart = GetCartById(id);
			cart.Products.Remove(cart.Products.FirstOrDefault(x => x.Equals(productId)));
			return HttpStatusCode.OK;
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="id"></param>
		/// <returns></returns>
		private CartModel GetCartById(Guid id)
		{
			return _carts.First(x => x.id == id);

		}
		/// <summary>
		/// 
		/// </summary>
		/// <param name="id"></param>
		/// <returns></returns>
		private object AddProductToCart(Guid id)
		{
			var request = this.Bind<AddProductRequest>();
			var cart = GetCartById(id);
			cart.Products.Add(request.productId);
			return HttpStatusCode.OK;

		}
		/// <summary>
		/// 
		/// </summary>
		/// <returns></returns>
		private object CreateCart()
		{
			var cartModel = new CartModel() { id = new Guid(), Products = new List<Guid>() };
			_carts.Add(cartModel);
			return cartModel;
		}
	}
}