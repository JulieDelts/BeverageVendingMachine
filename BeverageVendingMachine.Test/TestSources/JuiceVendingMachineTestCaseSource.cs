using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BeverageVendingMachine.DrinkModels;

using BeverageVendingMachine.VendingMachines;

namespace BeverageVendingMachine.Test
{
    namespace JuiceVendingMachineTestCaseSource
    {
        public class SellTestCaseSource : IEnumerable
        {
            public IEnumerator GetEnumerator()
            {
                string name = "pomegranate juice";

                DrinkTypesStorage<Juice> juiceTypesStorage = new("C:\\Users\\Юлия\\Desktop\\Программули\\BeverageVendingMachine\\JuiceTypes.txt");
                var juiceMachine = new JuiceVendingMachine(123456, 10, juiceTypesStorage);
                juiceMachine.Load();

                AbstractDrink drink = new Juice() { Type = "pomegranate", FruitAmountNeeded = 2, Price = 300, Name = "pomegranate juice" };

                yield return new object[] { name, juiceMachine, drink };
            }
        }

        public class SellWhenNoDrinkThenErrorTestCaseSource : IEnumerable
        {
            public IEnumerator GetEnumerator()
            {
                string name = "qwe juice";

                DrinkTypesStorage<Juice> juiceTypesStorage = new("C:\\Users\\Юлия\\Desktop\\Программули\\BeverageVendingMachine\\JuiceTypes.txt");
                var juiceMachine = new JuiceVendingMachine(123456, 10, juiceTypesStorage);

                juiceMachine.Load();

                yield return new object[] { name, juiceMachine};
            }
        }

        public class SellWhenNoIngredientsThenErrorTestCaseSource : IEnumerable
        {
            public IEnumerator GetEnumerator()
            {
                string name = "orange juice";

                DrinkTypesStorage<Juice> juiceTypesStorage = new("C:\\Users\\Юлия\\Desktop\\Программули\\BeverageVendingMachine\\JuiceTypes.txt");
                var juiceMachine = new JuiceVendingMachine(123456, 10, juiceTypesStorage);

                yield return new object[] { name, juiceMachine };
            }
        }

        public class SellWhenMachineBrokenThenErrorTestCaseSource : IEnumerable
        {
            public IEnumerator GetEnumerator()
            {
                string name = "orange juice";

                DrinkTypesStorage<Juice> juiceTypesStorage = new("C:\\Users\\Юлия\\Desktop\\Программули\\BeverageVendingMachine\\JuiceTypes.txt");
                var juiceMachine = new JuiceVendingMachine(123456, 10, juiceTypesStorage);
                juiceMachine.Load();
                juiceMachine.CurrentPurchaseCount = 10;

                yield return new object[] { name, juiceMachine };
            }
        }

        public class LoadTestCaseSource : IEnumerable
        {
            public IEnumerator GetEnumerator()
            {
                DrinkTypesStorage<Juice> juiceTypesStorage = new("C:\\Users\\Юлия\\Desktop\\Программули\\BeverageVendingMachine\\JuiceTypes.txt");
                var juiceMachine = new JuiceVendingMachine(123456, 10, juiceTypesStorage);

                Dictionary<string, int> fruitAmount = new() { {"orange juice", 15}, {"apple juice", 15}, {"pomegranate juice", 15}, {"grape juice", 15 }};
                var expected = new JuiceVendingMachine(123456, 10, juiceTypesStorage, 60, 60, fruitAmount);

                yield return new object[] { juiceMachine, expected };
            }
        }

        public class SetDrinkTypesStorageTestCaseSource : IEnumerable
        {
            public IEnumerator GetEnumerator()
            {
                string path1 = "C:\\Users\\Юлия\\Desktop\\Программули\\BeverageVendingMachine\\TypesStorageTest.txt";

                DrinkTypesStorage<Juice> juiceTypesStorage1 = new("C:\\Users\\Юлия\\Desktop\\Программули\\BeverageVendingMachine\\JuiceTypes.txt");
                var juiceMachine1 = new JuiceVendingMachine(123456, 10, juiceTypesStorage1);

                DrinkTypesStorage<Juice> juiceTypesStorageTest = new("C:\\Users\\Юлия\\Desktop\\Программули\\BeverageVendingMachine\\TypesStorageTest.txt");
                var expected1 = new JuiceVendingMachine(123456, 10, juiceTypesStorageTest);

                yield return new object[] { path1, juiceMachine1, expected1 };

                string path2 = "C:\\Users\\Юлия\\Desktop\\Программули\\BeverageVendingMachine\\QweQwe.txt";

                DrinkTypesStorage<Juice> juiceTypesStorage2 = new("C:\\Users\\Юлия\\Desktop\\Программули\\BeverageVendingMachine\\JuiceTypes.txt");
                var juiceMachine2 = new JuiceVendingMachine(123456, 10, juiceTypesStorage1);

                var expected2 = new JuiceVendingMachine(123456, 10, juiceTypesStorage2);

                yield return new object[] { path2, juiceMachine2, expected2 };
            }
        }

    }
}
