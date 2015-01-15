using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CartService.MessageContracts
{
	public interface ICartPaymentResult
	{
		Guid CartID { get; }
		bool PaymentSuccessful { get; }
	}
}