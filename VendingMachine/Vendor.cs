using System.Collections.Generic;
using System.Linq;

namespace VendingMachine
{
    public class Vendor
    {
        private const string InsufficientFundsMessage = "Insufficient Funds to return. Please try again later";

        public VendResult Vend(IList<Money> loadedMoney, Money input, int purchasePrice)
        {
            // Make a new copy of loaded money in case insufficient change to return
            var originalLoadedMoney = loadedMoney.Select(l => new Money { Denomination = l.Denomination, Count = l.Count }).ToList();

            // Start from highest denomination and proceed further so that largest denomination go first
            loadedMoney = loadedMoney.OrderByDescending(m => m.Denomination).ToList();

            VendResult vend;
            if (!ValidateLoadedMoney(loadedMoney, input, purchasePrice, out vend)) return vend;

           
            var result = new List<Money>();
            var changeToGive = input.TotalFunds - purchasePrice;

            foreach (var money in loadedMoney)
            {
                var remainder = changeToGive % money.Denomination;
                var quo = changeToGive / money.Denomination;

                if (remainder >= changeToGive) continue; // When number of coins present for denomination is not enough to dispense the remaining money or part of

                if (quo > money.Count) // If program tries to take more than present for that denomination
                {
                    quo = money.Count;
                    changeToGive = changeToGive - money.TotalFunds;
                    money.Count = 0;
                }
                else // Change possible from given denomination
                {
                    changeToGive = remainder;
                    money.Count -= quo;
                }

                result.Add(new Money { Denomination = money.Denomination, Count = quo });
            }

            if (changeToGive == 0) // Change successfully found from available denominations
                return new VendResult
                {
                    MoneyLeft = loadedMoney,
                    Message = string.Join(",", result.Select(r => r.ToString()))
                };

            // No matching combination found for dispensing the change
            return new VendResult { Message = InsufficientFundsMessage, MoneyLeft = originalLoadedMoney.ToList() };
        }

        private static bool ValidateLoadedMoney(IList<Money> loadedMoney, Money input, int purchasePrice, out VendResult vend)
        {
            vend = new VendResult();
            if (!loadedMoney.Any() && input.TotalFunds == purchasePrice)
            {
                {
                    vend = new VendResult { Message = "No Change given" };
                    return false;
                }
            }

            if (loadedMoney.Any() || input.TotalFunds <= purchasePrice) return true;

            vend = new VendResult { Message = InsufficientFundsMessage };
            return false;

        }
    }
}
