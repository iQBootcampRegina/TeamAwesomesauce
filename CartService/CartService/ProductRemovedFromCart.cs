using System;

namespace CartService
{
	public class ProductRemovedFromCart : ProductInCart
	{
		public ProductRemovedFromCart(Guid id, Guid productId) : base(id,productId)
		{

		}
	}
}