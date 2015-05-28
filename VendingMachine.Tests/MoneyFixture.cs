using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

namespace VendingMachine.Tests
{
    public class MoneyFixture
    {
        [Test]
        public void GivenMoneyWhenToStringIsCalledThenExpectedMessageComesBack()
        {
            var money = new Money { Denomination = 50, Count = 2 };

            Assert.AreEqual("50 x 2", money.ToString());
        }
    }
}
