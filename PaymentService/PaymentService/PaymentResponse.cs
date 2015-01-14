using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PaymentService
{
	public class PaymentResponse
	{
		public Guid OrderID { get; set; }
		public Guid? PaymentConfirmationNumber { get; set; }
	}
}