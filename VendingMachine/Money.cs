using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.AccessControl;
using System.Text;
using System.Threading.Tasks;

namespace VendingMachine
{
    public class Money
    {
        public int Denomination { get; set; }
        public int Count { get; set; }


        public override string ToString()
        {
            return string.Format("{0} x {1}", Denomination, Count);
        }

        public int TotalFunds { get { return Denomination * Count; } }
    }
}
