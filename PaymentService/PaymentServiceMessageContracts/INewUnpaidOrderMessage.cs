using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PaymentServiceMessageContracts
{
    public interface INewUnpaidOrderMessage
    {
		Guid CartID { get; }
		decimal AmountDue { get; }
    }
}
