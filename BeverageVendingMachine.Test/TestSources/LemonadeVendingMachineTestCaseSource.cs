using System.Collections;
using BeverageVendingMachine.DrinkModels;
using BeverageVendingMachine.VendingMachines;

namespace BeverageVendingMachine.Test
{
    namespace LemonadeVendingMachineTestCaseSource
    {
        public class SellTestCaseSource : IEnumerable
        {
            public IEnumerator GetEnumerator()
            {
                string name = "fanta";

                DrinkTypesStorage<Lemonade> lemonadeTypesStorage = new("C:\\Users\\Юлия\\Desktop\\Программули\\BeverageVendingMachine\\LemonadeTypes.txt");
                var lemonadeMachine = new LemonadeVendingMachine(123456, 10, lemonadeTypesStorage);
                lemonadeMachine.Load();

                AbstractDrink drink = new Lemonade() { Name = "fanta", Price = 100 };

                yield return new object[] { name, lemonadeMachine, drink };
            }
        }

        public class SellWhenNoDrinkThenErrorTestCaseSource : IEnumerable
        {
            public IEnumerator GetEnumerator()
            {
                string name = "chernogolovka";

                DrinkTypesStorage<Lemonade> lemonadeTypesStorage = new("C:\\Users\\Юлия\\Desktop\\Программули\\BeverageVendingMachine\\LemonadeTypes.txt");
                var lemonadeMachine = new LemonadeVendingMachine(123456, 10, lemonadeTypesStorage);

                lemonadeMachine.Load();

                yield return new object[] { name, lemonadeMachine };
            }
        }

        public class SellWhenNoIngredientsThenErrorTestCaseSource : IEnumerable
        {
            public IEnumerator GetEnumerator()
            {
                string name = "cola";

                DrinkTypesStorage<Lemonade> lemonadeTypesStorage = new("C:\\Users\\Юлия\\Desktop\\Программули\\BeverageVendingMachine\\LemonadeTypes.txt");
                var lemonadeMachine = new LemonadeVendingMachine(123456, 10, lemonadeTypesStorage);

                yield return new object[] { name, lemonadeMachine };
            }
        }

        public class SellWhenMachineBrokenThenErrorTestCaseSource : IEnumerable
        {
            public IEnumerator GetEnumerator()
            {
                string name = "cola";

                DrinkTypesStorage<Lemonade> lemonadeTypesStorage = new("C:\\Users\\Юлия\\Desktop\\Программули\\BeverageVendingMachine\\LemonadeTypes.txt");
                var lemonadeMachine = new LemonadeVendingMachine(123456, 10, lemonadeTypesStorage);
                lemonadeMachine.Load();
                lemonadeMachine.CurrentPurchaseCount = 10;

                yield return new object[] { name, lemonadeMachine };
            }
        }

        public class LoadTestCaseSource : IEnumerable
        {
            public IEnumerator GetEnumerator()
            {
                DrinkTypesStorage<Lemonade> lemonadeTypesStorage = new("C:\\Users\\Юлия\\Desktop\\Программули\\BeverageVendingMachine\\LemonadeTypes.txt");
                var lemonadeMachine = new LemonadeVendingMachine(123456, 10, lemonadeTypesStorage);

                Dictionary<string, int> numberOfCansPerType = new() { { "fanta", 20 }, { "sprite", 20 }, { "cola", 20 }};
                var expected = new LemonadeVendingMachine(123456, 10, lemonadeTypesStorage, 60, numberOfCansPerType);

                yield return new object[] { lemonadeMachine, expected };
            }
        }

        public class SetDrinkTypesStorageTestCaseSource : IEnumerable
        {
            public IEnumerator GetEnumerator()
            {
                string path1 = "C:\\Users\\Юлия\\Desktop\\Программули\\BeverageVendingMachine\\TypesStorageTest.txt";

                DrinkTypesStorage<Lemonade> lemonadeTypesStorage1 = new("C:\\Users\\Юлия\\Desktop\\Программули\\BeverageVendingMachine\\LemonadeTypes.txt");
                var lemonadeMachine1 = new LemonadeVendingMachine(123456, 10, lemonadeTypesStorage1);

                DrinkTypesStorage<Lemonade> lemonadeTypesStorageTest = new("C:\\Users\\Юлия\\Desktop\\Программули\\BeverageVendingMachine\\TypesStorageTest.txt");
                var expected1 = new LemonadeVendingMachine(123456, 10, lemonadeTypesStorageTest);

                yield return new object[] { path1, lemonadeMachine1, expected1 };

                string path2 = "C:\\Users\\Юлия\\Desktop\\Программули\\BeverageVendingMachine\\QweQwe.txt";

                DrinkTypesStorage<Lemonade> lemonadeTypesStorage2 = new("C:\\Users\\Юлия\\Desktop\\Программули\\BeverageVendingMachine\\LemonadeTypes.txt");
                var lemonadeMachine2 = new LemonadeVendingMachine(123456, 10, lemonadeTypesStorage1);

                var expected2 = new LemonadeVendingMachine(123456, 10, lemonadeTypesStorage2);

                yield return new object[] { path2, lemonadeMachine2, expected2 };
            }
        }
    }


}
