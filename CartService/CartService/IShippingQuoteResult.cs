using System;

namespace CartService
{
	public interface IShippingQuoteResult
	{
		Guid CartId { get; set; }
		decimal Price { get; set; }
	}
}