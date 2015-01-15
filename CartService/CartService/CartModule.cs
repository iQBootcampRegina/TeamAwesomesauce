using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using IQ.Foundation.Messaging;
using IQ.Foundation.Messaging.AzureServiceBus;
using Nancy;
using Nancy.ModelBinding;
using PaymentServiceMessageContracts;

namespace CartService
{
	public class CartModule : NancyModule
	{
		IPublishMessages messagePublisher;
		IEnqueueMessages messageQueuePublisher;
		//Nothing wrong with this.
		private static readonly List<CartModel> _carts = new List<CartModel>();
		private static Dictionary<Guid, decimal> _shippingPrices = new Dictionary<Guid, decimal>();

		public CartModule(IPublishMessages publisher, IEnqueueMessages queue )
		{
			Post["/Carts"] = x => CreateCart();
			Post["/Carts/{id}/Products"] = x => AddProductToCart(x.id);
			Get["/Carts/{id}"] = x => GetCartById(x.id);
			Delete["/Carts/{id}/Products/{productId}"] = x => RemoveProductFromCart(x.id, x.productId);
			Post["/Carts/{id}/Checkout"] = x => CheckoutCart(x.id);
			messagePublisher = publisher;
			messageQueuePublisher = queue;

		}



		/// <summary>
		///
		/// </summary>
		/// <param name="id"></param>
		/// <returns></returns>
		private object CheckoutCart(Guid id)
		{
			decimal amount = GetFinalAmount(id);
			PublishUtility.PublishMessage(messagePublisher, new CartCheckoutModel(id, amount));

			return HttpStatusCode.OK;

		}

		/// <summary>
		///
		/// </summary>
		/// <param name="cartId"></param>
		/// <returns></returns>
		private decimal GetFinalAmount(Guid cartId)
		{
			//total products + shipping
			var cart = GetCartById(cartId) as CartModel;
			var price = 0m;
			if (cart != null)
			{
				price = cart.Products.Sum(x => GetProductPrice(x));
			}
			else
			{
				//yes
				throw new KeyNotFoundException();
			}

			price += GetShippingPrice(cartId);

			return price;

		}

		/// <summary>
		///
		/// </summary>
		/// <param name="cartId"></param>
		/// <returns></returns>
		private decimal GetShippingPrice(Guid cartId)
		{
			if (_shippingPrices.ContainsKey(cartId))
				return _shippingPrices[cartId];
			//great deal
			return 9.99m;
		}

		private decimal GetProductPrice(Guid guid)
		{
			return 10m;
		}

		/// <summary>
		///
		/// </summary>
		/// <param name="message"></param>
		public void PaymentCompletedHandler(IPaymentCompleteModel message)
		{
			// Move cart to paid - notify it is paid
			CartModel cartById = (CartModel)GetCartById(message.CartID);
			if (cartById != null)
			{
				PublishUtility.PublishMessage(messageQueuePublisher, cartById);
				_carts.Remove(cartById);
			}

		}


		/// <summary>
		///
		/// </summary>
		/// <param name="message"></param>
		public void ShippingPriceUpdateddHandler(ShippingQuoteResult message)
		{
			if (_shippingPrices.ContainsKey(message.CartId))
			{
				_shippingPrices[message.CartId] = message.Price;
			}
			else
			{
				_shippingPrices.Add(message.CartId, message.Price);
			}
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

			PublishUtility.PublishMessage(messagePublisher, new ProductRemovedFromCart(cart.id, productId));

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
			Guid productId = request.ProductId;
			if (cart != null && productId != Guid.Empty)
			{

				cart.Products.Add(productId);
						//Call Add product to cart

			PublishUtility.PublishMessage(messagePublisher, new ProductAddedToCart(cart.id, productId));
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