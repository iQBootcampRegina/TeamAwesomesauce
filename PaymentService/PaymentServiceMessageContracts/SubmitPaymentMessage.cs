﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PaymentServiceMessageContracts;

namespace PaymentServiceMessageContracts
{
	public class SubmitPaymentMessage
	{
		public SubmitPaymentMessage()
		{
			
		}
		
		public SubmitPaymentMessage(Guid cartID, string billingInfo, decimal amount)
		{
			this.CartID = cartID;
			this.BillingInfo = billingInfo;
			this.Amount = amount;
		}
		
		public Guid CartID { get; private set; }
		public string BillingInfo { get; private set; }
		public decimal Amount { get; private set; }
	}
}
