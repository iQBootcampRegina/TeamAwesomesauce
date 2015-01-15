using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PaymentService
{
	/// <summary>
	/// Stores a collection of unpaid carts and their associated amounts.
	/// </summary>
	public static class UnpaidCartDataStore
	{
		private static readonly Dictionary<Guid, decimal> _unpaidCarts = new Dictionary<Guid, decimal>();
		
		/// <summary>
		/// Gets the amount associated with the unpaid cart.  If no cart matches the specified ID, return null.
		/// </summary>
		/// <param name="cartID">The cart ID.</param>
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
		/// <param name="cartID">The cart ID.</param>
		/// <param name="amount">Amount due.</param>
		/// <remarks>
		/// This method creates a new unpaid cart if the ID does not already exist; otherwise, it updates the existing record.
		/// </remarks>
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
		/// <returns>TRUE if the cart was successfully paid in full; otherwise, FALSE.</returns>
		public static bool SetOrderAsPaid(Guid cartID)
		{
			return _unpaidCarts.Remove(cartID);
		}
	}
}