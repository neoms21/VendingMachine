using System;
using System.Collections.Generic;
using System.Linq;

namespace VendingMachine.Console
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                var loadedMoney = new List<Money>();
                System.Console.WriteLine("Enter No of 200 (£2) coins");
                var coins = System.Console.ReadLine();
                AddInputMoney(loadedMoney, 200, coins);
                System.Console.WriteLine("Enter No of 100 (£1) coins");
                coins = System.Console.ReadLine();
                AddInputMoney(loadedMoney, 100, coins);
                System.Console.WriteLine("Enter No of 50p coins");
                coins = System.Console.ReadLine();
                AddInputMoney(loadedMoney, 50, coins);
                System.Console.WriteLine("Enter No of 20p coins");
                coins = System.Console.ReadLine();
                AddInputMoney(loadedMoney, 20, coins);
                System.Console.WriteLine("Enter No of 10p coins");
                coins = System.Console.ReadLine();
                AddInputMoney(loadedMoney, 10, coins);
                System.Console.WriteLine("Enter No of 5p coins");
                coins = System.Console.ReadLine();
                AddInputMoney(loadedMoney, 5, coins);
                System.Console.WriteLine("Enter No of 2p coins");
                coins = System.Console.ReadLine();
                AddInputMoney(loadedMoney, 2, coins);
                System.Console.WriteLine("Enter No of 1p coins");
                coins = System.Console.ReadLine();
                AddInputMoney(loadedMoney, 1, coins);

                System.Console.WriteLine("Enter Denomination for purchase: ");
                var denomination = System.Console.ReadLine();

                System.Console.WriteLine("Enter No of denominations for purchase: ");
                var count = System.Console.ReadLine();

                var inputMoney = new Money { Denomination = Int32.Parse(denomination), Count = Int32.Parse(count) };

                System.Console.WriteLine("Enter Purchase price: ");
                var purchasePrice = System.Console.ReadLine();


                var vendor = new Vendor();
                var result = vendor.Vend(loadedMoney, inputMoney, Int32.Parse(purchasePrice));
                System.Console.WriteLine("Denominations Returned " + result.Message);
                System.Console.WriteLine("----------------------Money Left in Machine-------------------");
                System.Console.WriteLine(string.Join(",\n", result.MoneyLeft.Select(r => r.ToString())));

                System.Console.ReadLine();
            }
            catch (Exception e)
            {
                System.Console.WriteLine("Please enter valid number value");
                System.Console.WriteLine(e.ToString());
                System.Console.ReadLine();
            }
        }

        private static void AddInputMoney(List<Money> inputMoney, int denomination, string coins)
        {
            inputMoney.Add(new Money { Denomination = denomination, Count = Int32.Parse(coins) });
        }
    }
}
