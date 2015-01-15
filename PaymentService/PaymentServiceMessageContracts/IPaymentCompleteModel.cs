using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PaymentServiceMessageContracts
{
	public interface IPaymentCompleteModel
	{
		Guid CartID { get; }
	}
}
