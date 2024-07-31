using System.Collections;
using BeverageVendingMachine.DrinkModels;
using BeverageVendingMachine.VendingMachines;

namespace BeverageVendingMachine.Test
{
    namespace VendingMachineManagerTestCaseSource
    {
        public class GetTestCaseSource : IEnumerable
        {
            public IEnumerator GetEnumerator()
            {
                int id = 231445;

                DrinkTypesStorage<Juice> juiceTypesStorage = new("C:\\Users\\Юлия\\Desktop\\Программули\\BeverageVendingMachine\\JuiceTypes.txt");
                DrinkTypesStorage<Coffee> coffeeTypesStorage = new("C:\\Users\\Юлия\\Desktop\\Программули\\BeverageVendingMachine\\CoffeeTypes.txt");

                var juiceMachine = new JuiceVendingMachine(123456, 10, juiceTypesStorage);
                var coffeeMachine = new CoffeeVendingMachine(231445, 10, coffeeTypesStorage);

                List<AbstractVendingMachine> list = new() { juiceMachine, coffeeMachine };
                VendingMachineManager manager = new(list);

                yield return new object[] { id, manager, coffeeMachine };
            }
        }

        public class GetWhenNoMachineThenExceptionTestCaseSource : IEnumerable
        {
            public IEnumerator GetEnumerator()
            {
                int id = 222223;

                DrinkTypesStorage<Juice> juiceTypesStorage = new("C:\\Users\\Юлия\\Desktop\\Программули\\BeverageVendingMachine\\JuiceTypes.txt");
                DrinkTypesStorage<Coffee> coffeeTypesStorage = new("C:\\Users\\Юлия\\Desktop\\Программули\\BeverageVendingMachine\\CoffeeTypes.txt");

                var juiceMachine = new JuiceVendingMachine(123456, 10, juiceTypesStorage);
                var coffeeMachine = new CoffeeVendingMachine(231445, 10, coffeeTypesStorage);

                List<AbstractVendingMachine> list = new() { juiceMachine, coffeeMachine };
                VendingMachineManager manager = new(list);

                yield return new object[] { id, manager };
            }
        }

        public class AddTestCaseSource : IEnumerable
        {
            public IEnumerator GetEnumerator()
            {
                DrinkTypesStorage<Juice> drinkTypesStorage = new("C:\\Users\\Юлия\\Desktop\\Программули\\BeverageVendingMachine\\JuiceTypes.txt");

                AbstractVendingMachine machine = new JuiceVendingMachine(123456, 10, drinkTypesStorage);

                VendingMachineManager manager = new();

                List<AbstractVendingMachine> list = new() { machine };

                VendingMachineManager expected = new(list);

                yield return new object[] { machine, manager, expected };
            }
        }

        public class AddRangeTestCaseSource : IEnumerable
        {
            public IEnumerator GetEnumerator()
            {
                DrinkTypesStorage<Juice> juiceTypesStorage = new("C:\\Users\\Юлия\\Desktop\\Программули\\BeverageVendingMachine\\JuiceTypes.txt");
                DrinkTypesStorage<Coffee> coffeeTypesStorage = new("C:\\Users\\Юлия\\Desktop\\Программули\\BeverageVendingMachine\\CoffeeTypes.txt");

                var juiceMachine = new JuiceVendingMachine(123456, 10, juiceTypesStorage);
                var coffeeMachine = new CoffeeVendingMachine(677895, 10, coffeeTypesStorage);

                VendingMachineManager manager = new();

                List<AbstractVendingMachine> list = new() { juiceMachine, coffeeMachine };
                VendingMachineManager expected = new(list);

                yield return new object[] { list, manager, expected };
            }
        }

        public class RemoveTestCaseSource : IEnumerable
        {
            public IEnumerator GetEnumerator()
            {
                int id = 123456;

                DrinkTypesStorage<Juice> juiceTypesStorage = new("C:\\Users\\Юлия\\Desktop\\Программули\\BeverageVendingMachine\\JuiceTypes.txt");
                DrinkTypesStorage<Coffee> coffeeTypesStorage = new("C:\\Users\\Юлия\\Desktop\\Программули\\BeverageVendingMachine\\CoffeeTypes.txt");

                var juiceMachine = new JuiceVendingMachine(123456, 10, juiceTypesStorage);
                var coffeeMachine = new CoffeeVendingMachine(677895, 10, coffeeTypesStorage);

                List<AbstractVendingMachine> list = new() { juiceMachine, coffeeMachine };

                VendingMachineManager manager = new(list);
                VendingMachineManager expected = new();
                expected.Add(coffeeMachine);

                yield return new object[] { id, manager, expected };
            }
        }

        public class LoadAllTestCaseSource : IEnumerable
        {
            public IEnumerator GetEnumerator()
            {
                DrinkTypesStorage<Juice> juiceTypesStorage = new("C:\\Users\\Юлия\\Desktop\\Программули\\BeverageVendingMachine\\JuiceTypes.txt");
                DrinkTypesStorage<Coffee> coffeeTypesStorage = new("C:\\Users\\Юлия\\Desktop\\Программули\\BeverageVendingMachine\\CoffeeTypes.txt");
                DrinkTypesStorage<Lemonade> lemonadeTypesStorage = new("C:\\Users\\Юлия\\Desktop\\Программули\\BeverageVendingMachine\\LemonadeTypes.txt");
                DrinkTypesStorage<Tea> teaTypesStorage = new("C:\\Users\\Юлия\\Desktop\\Программули\\BeverageVendingMachine\\TeaTypes.txt");

                var juiceMachine = new JuiceVendingMachine(123456, 10, juiceTypesStorage);
                var coffeeMachine = new CoffeeVendingMachine(677895, 10, coffeeTypesStorage);
                var lemonadeMachine = new LemonadeVendingMachine(4578744, 10, lemonadeTypesStorage);
                var coffeeTeaChocolateMachine = new CoffeeTeaHotChocolateVendingMachine(456709, coffeeTypesStorage, 10, teaTypesStorage);

                List<AbstractVendingMachine> list = new() { juiceMachine, coffeeMachine, lemonadeMachine, coffeeTeaChocolateMachine };
                VendingMachineManager manager = new(list);

                var expectedJuiceMachine = new JuiceVendingMachine(123456, 10, juiceTypesStorage);
                var expectedCoffeeMachine = new CoffeeVendingMachine(677895, 10, coffeeTypesStorage);
                var expectedLemonadeMachine = new LemonadeVendingMachine(4578744, 10, lemonadeTypesStorage);
                var expectedCoffeeTeaChocolateMachine = new CoffeeTeaHotChocolateVendingMachine(456709, coffeeTypesStorage, 10, teaTypesStorage);

                expectedJuiceMachine.Load();
                expectedLemonadeMachine.Load();
                expectedCoffeeMachine.Load();
                expectedCoffeeTeaChocolateMachine.Load();

                List<AbstractVendingMachine> expectedList = new() { expectedJuiceMachine, expectedCoffeeMachine, expectedLemonadeMachine, expectedCoffeeTeaChocolateMachine };
                var expected = new VendingMachineManager(expectedList);

                yield return new object[] { manager, expected };
            }
        }

        public class RepairAllTestCaseSource : IEnumerable
        {
            public IEnumerator GetEnumerator()
            {
                DrinkTypesStorage<Juice> juiceTypesStorage = new("C:\\Users\\Юлия\\Desktop\\Программули\\BeverageVendingMachine\\JuiceTypes.txt");
                DrinkTypesStorage<Coffee> coffeeTypesStorage = new("C:\\Users\\Юлия\\Desktop\\Программули\\BeverageVendingMachine\\CoffeeTypes.txt");
                DrinkTypesStorage<Lemonade> lemonadeTypesStorage = new("C:\\Users\\Юлия\\Desktop\\Программули\\BeverageVendingMachine\\LemonadeTypes.txt");
                DrinkTypesStorage<Tea> teaTypesStorage = new("C:\\Users\\Юлия\\Desktop\\Программули\\BeverageVendingMachine\\TeaTypes.txt");

                var juiceMachine = new JuiceVendingMachine(123456, 2, juiceTypesStorage);
                var coffeeMachine = new CoffeeVendingMachine(677895, 2, coffeeTypesStorage);
                var lemonadeMachine = new LemonadeVendingMachine(4578744, 2, lemonadeTypesStorage);
                var coffeeTeaChocolateMachine = new CoffeeTeaHotChocolateVendingMachine(456709, coffeeTypesStorage, 2, teaTypesStorage);

                juiceMachine.CurrentPurchaseCount = 2;
                coffeeMachine.CurrentPurchaseCount = 2;
                lemonadeMachine.CurrentPurchaseCount = 2;
                coffeeTeaChocolateMachine.CurrentPurchaseCount = 2;

                List<AbstractVendingMachine> list = new() { juiceMachine, coffeeMachine, lemonadeMachine, coffeeTeaChocolateMachine };
                VendingMachineManager manager = new(list);

                var expectedJuiceMachine = new JuiceVendingMachine(123456, 2, juiceTypesStorage);
                var expectedCoffeeMachine = new CoffeeVendingMachine(677895, 2, coffeeTypesStorage);
                var expectedLemonadeMachine = new LemonadeVendingMachine(4578744, 2, lemonadeTypesStorage);
                var expectedCoffeeTeaChocolateMachine = new CoffeeTeaHotChocolateVendingMachine(456709, coffeeTypesStorage, 2, teaTypesStorage);

                List<AbstractVendingMachine> expectedList = new() { expectedJuiceMachine, expectedCoffeeMachine, expectedLemonadeMachine, expectedCoffeeTeaChocolateMachine };
                var expected = new VendingMachineManager(expectedList);

                yield return new object[] { manager, expected };
            }
        }
    }
}
