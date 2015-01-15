using System;

namespace CartService
{
	public class ProductInCart
	{
		public Guid CartId { get; set; }
		public Guid ProductId { get; set; }
		public ProductInCart(Guid id, Guid productId)
		{
			CartId = id;
			ProductId = productId;
		}
	}
}