using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PaymentService
{
	public static class UnpaidCartDataStore
	{
		private static readonly Dictionary<Guid, decimal> _unpaidCarts = new Dictionary<Guid, decimal>();

		public static IReadOnlyDictionary<Guid, decimal> UnpaidCarts { get { return _unpaidCarts; } }

		/// <summary>
		/// Obtains the amount associated with the unpaid cart.  If no order, return null.
		/// </summary>
		/// <param name="cartID"></param>
		/// <returns></returns>
		public static decimal? GetAmount(Guid cartID)
		{
			if (_unpaidCarts.ContainsKey(cartID))
				return _unpaidCarts[cartID];
				
			return null;
		}

		/// <summary>
		/// Set the amount associated with an unpaid cart.
		/// </summary>
		/// <param name="cartID">Unpaid cart ID.</param>
		/// <param name="amount">Amount due.</param>
		public static void SetAmount(Guid cartID, decimal amount)
		{
			if (_unpaidCarts.ContainsKey(cartID))
				_unpaidCarts[cartID] = amount;
			else
				_unpaidCarts.Add(cartID, amount);
		}

		/// <summary>
		/// Pay for a cart.
		/// </summary>
		/// <param name="cartID">Cart ID.</param>
		/// <returns></returns>
		public static bool SetOrderAsPaid(Guid cartID)
		{
			return _unpaidCarts.Remove(cartID);
		}
	}
}