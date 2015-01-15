using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using IQ.Foundation.Messaging;
using IQ.Foundation.Messaging.AzureServiceBus;
using Nancy;
using Nancy.ModelBinding;

namespace CartService
{
	public class CartModule : NancyModule
	{
		IPublishMessages messagePublisher;
		DefaultAzureServiceBusBootstrapper bootstrapper;
		//Nothing wrong with this.
		private static readonly List<CartModel> _carts = new List<CartModel>();


		public CartModule()
		{
			Post["/Carts"] = x => CreateCart();
			Post["/Carts/{id}/Products"] = x => AddProductToCart(x.id);
			Get["/Carts/{id}"] = x => GetCartById(x.id);
			Delete["/Carts/{id}/Products/{productId}"] = x => RemoveProductFromCart(x.id, x.productId);



			bootstrapper = new DefaultAzureServiceBusBootstrapper(new CartPublisherConfiguration());


			messagePublisher = bootstrapper.BuildMessagePublisher();
		}

		/// <summary>
		/// 
		/// <param name="id"></param>
		/// <param name="productId"></param>
		/// <returns></returns>
		private object RemoveProductFromCart(Guid id, Guid productId)
		{
			CartModel cart = (CartModel)GetCartById(id);
			if (cart != null)
			{
				cart.Products.Remove(cart.Products.FirstOrDefault(x => x.Equals(productId)));
				return HttpStatusCode.OK;
			}

			PublishMessage(messagePublisher, new ProductRemovedFromCart(cart.id, productId));

			return HttpStatusCode.NotFound;

		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="id"></param>
		/// <returns></returns>
		private object GetCartById(Guid id)
		{
			var cart = _carts.FirstOrDefault(x => x.id == id);
			if (cart != null)
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
			var cart = (CartModel)GetCartById(id);
			if (cart != null)
			{
				cart.Products.Add(request.productId);
				return HttpStatusCode.OK;
			}
			//Call Add product to cart

			PublishMessage(messagePublisher, new ProductAddedToCart(cart.id, request.productId));

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

		private static void PublishMessage(IPublishMessages messagePublisher, object message)
		{
			Console.WriteLine(message.ToString());
			messagePublisher.Publish(message);
		}
	}
}