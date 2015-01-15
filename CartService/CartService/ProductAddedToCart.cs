using System;

namespace CartService
{
	public class ProductAddedToCart : ProductInCart
	{
		public ProductAddedToCart(Guid id, Guid productId) : base(id, productId)
		{
		}
	}
}