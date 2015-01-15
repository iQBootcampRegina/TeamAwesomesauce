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
			Get["/Carts/{id}"] = x => GetCartById(x.id);
			Delete["/Carts/{id}/Products/{productId}"] = x => RemoveProductFromCart(x.id, x.productId);

		}

		/// <summary>
		/// 
		/// <param name="id"></param>
		/// <param name="productId"></param>
		/// <returns></returns>
		private object RemoveProductFromCart(Guid id, Guid productId)
		{
			CartModel cart = (CartModel) GetCartById(id);
			if (cart != null)
			{
				cart.Products.Remove(cart.Products.FirstOrDefault(x => x.Equals(productId)));
				return HttpStatusCode.OK;
			}
			return HttpStatusCode.NotFound;
			
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="id"></param>
		/// <returns></returns>
		private object GetCartById(Guid id)
		{
			var cart =  _carts.FirstOrDefault(x => x.id == id);
			if(cart != null)
			{
				return cart;
			}
			return HttpStatusCode.NotFound;

		}
		/// <summary>
		/// 
		/// </summary>
		/// <param name="id"></param>
		/// <returns></returns>
		private object AddProductToCart(Guid id)
		{
			var request = this.Bind<AddProductRequest>();
			CartModel cart = (CartModel)GetCartById(id);
			if (cart != null)
			{
				cart.Products.Add(request.productId);
				return HttpStatusCode.OK;
			}
			return HttpStatusCode.NotFound;

		}
		/// <summary>
		/// 
		/// </summary>
		/// <returns></returns>
		private object CreateCart()
		{
			var cartModel = new CartModel() { id = Guid.NewGuid(), Products = new List<Guid>() };
			_carts.Add(cartModel);
			return cartModel;
		}
	}
}