using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;

namespace VendingMachine.Tests
{
    [TestFixture]
    public class VendorFixture
    {
        private Vendor _vendor;

        [SetUp]
        public void Setup()
        {
            _vendor = new Vendor();
        }

        [Test]
        public void GivenWhenThereIsNoCurrencyInMachineAndPurchasePriceIsEqualToInputThenPurchaseIsSuccessfullyMade()
        {
            var output = _vendor.Vend(new List<Money>(), new Money { Denomination = 20, Count = 3 }, 60);

            Assert.AreEqual("No Change given", output.Message);
        }

        [Test]
        public void GivenWhenThereIsNoCurrencyInMachineAndPurchaseIsMadeThenMessageIsDisplayedAsInsufficientFundsToVend()
        {
            var output = _vendor.Vend(new List<Money>(), new Money { Denomination = 20, Count = 3 }, 47);
            Assert.AreEqual("Insufficient Funds to return. Please try again later", output.Message);
        }

        [Test]
        public void GivenInitialMoneyIsLoadedAndPurchaseIsMadeButCorrectChangeIsNotAvailableToVendThenCorrectChangeIsReturned()
        {

            //ARRANGE
            var loadedMoney = new List<Money> { GetMoney(50, 2), GetMoney(20, 2), GetMoney(10, 3), GetMoney(5, 40), GetMoney(2, 30), GetMoney(1, 30) };

            //ACT
            var output = _vendor.Vend(loadedMoney, new Money { Denomination = 20, Count = 3 }, 47);

            //ASSERT MONEY THAT'S RETURNED
            Assert.AreEqual("10 x 1,2 x 1,1 x 1", output.Message);

            // ASSERT MONEY THAT'S LEFT IN THE MACHINE
            Assert.AreEqual("50 x 2,20 x 2,10 x 2,5 x 40,2 x 29,1 x 29", string.Join(",", output.MoneyLeft.Select(r => r.ToString())));
        }

        [Test]
        public void GivenInitialMoneyIsLoadedInUnorderedfashionAndPurchaseIsMadeThenCorrectChangeIsReturned()
        {

            //ARRANGE
            var loadedMoney = new List<Money> { GetMoney(10, 3), GetMoney(5, 40), GetMoney(50, 2), GetMoney(20, 2), GetMoney(2, 30), GetMoney(1, 30) };

            //ACT
            var output = _vendor.Vend(loadedMoney, new Money { Denomination = 20, Count = 3 }, 47);

            //ASSERT MONEY THAT'S RETURNED
            Assert.AreEqual("10 x 1,2 x 1,1 x 1", output.Message);

            // ASSERT MONEY THAT'S LEFT IN THE MACHINE
            Assert.AreEqual("50 x 2,20 x 2,10 x 2,5 x 40,2 x 29,1 x 29", string.Join(",", output.MoneyLeft.Select(r => r.ToString())));
        }

        [Test]
        public void GivenInitialMoneyIsLoadedAndPurchaseIsMadeButHigherDenominationRunsOutToVendThenCorrectChangeIsReturned()
        {
            //ARRANGE
            var loadedMoney = new List<Money> { GetMoney(50, 2), GetMoney(20, 2), GetMoney(10, 3), GetMoney(5, 40), GetMoney(2, 30), GetMoney(1, 30) };

            //ACT
            var output = _vendor.Vend(loadedMoney, new Money { Denomination = 50, Count = 5 }, 47);

            //ASSERT MONEY THAT'S RETURNED
            Assert.AreEqual("50 x 2,20 x 2,10 x 3,5 x 6,2 x 1,1 x 1", output.Message);

            // ASSERT MONEY THAT'S LEFT IN THE MACHINE
            Assert.AreEqual("50 x 0,20 x 0,10 x 0,5 x 34,2 x 29,1 x 29", string.Join(",", output.MoneyLeft.Select(r => r.ToString())));
        }

        [Test]
        public void GivenInitialMoneyIsLoadedAndPurchaseIsMadeButCorrectChangeIsNotAvailableToVendThenPurchaseIsCancelled()
        {
            //ARRANGE
            var loadedMoney = new List<Money> { GetMoney(50, 0), GetMoney(20, 0), GetMoney(10, 0), GetMoney(5, 3), GetMoney(2, 30), GetMoney(1, 30) };

            //ACT
            var output = _vendor.Vend(loadedMoney, new Money { Denomination = 50, Count = 5 }, 47);

            //ASSERT MONEY THAT'S RETURNED
            Assert.AreEqual("Insufficient Funds to return. Please try again later", output.Message);

            // ASSERT MONEY THAT'S LEFT IN THE MACHINE
            Assert.AreEqual("50 x 0,20 x 0,10 x 0,5 x 3,2 x 30,1 x 30", string.Join(",", output.MoneyLeft.Select(r => r.ToString())));
        }


        private Money GetMoney(int denomination, int count)
        {
            return new Money { Denomination = denomination, Count = count };
        }
    }

}
