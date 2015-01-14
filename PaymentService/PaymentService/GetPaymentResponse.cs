using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PaymentService
{
	public class GetPaymentRequest
	{
		public Guid OrderID { get; set; }
	}
}