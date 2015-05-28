using System.Collections.Generic;

namespace VendingMachine
{
    public class VendResult
    {
        public string Message { get; set; }

        public IList<Money> MoneyLeft { get; set; } 
    }
}
