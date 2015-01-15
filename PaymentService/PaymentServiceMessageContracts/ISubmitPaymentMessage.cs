using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PaymentServiceMessageContracts
{
	public interface ISubmitPaymentMessage
	{
		Guid CartID { get; }
		string BillingInfo { get; }
		decimal AmountDue { get; }
	}
}
