using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PaymentServiceMessageContracts
{
	public interface ICartPaymentResult
	{
		Guid CartID { get; }
		bool PaymentSuccessful { get; }
	}
}
