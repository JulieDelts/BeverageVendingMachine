using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BeverageVendingMachine.VendingMachines;
using BeverageVendingMachine.Test.LemonadeVendingMachineTestCaseSource;
using BeverageVendingMachine.DrinkModels;

namespace BeverageVendingMachine.Test.Tests
{
    public class LemonadeVendingMachineTests
    {
        [TestCaseSource(typeof(SellTestCaseSource))]
        public void SellTest(string name, LemonadeVendingMachine machine, AbstractDrink expected)
        {
            var drink = machine.Sell(name);

            var actual = drink;

            Assert.That(actual, Is.EqualTo(expected));
        }

        [TestCaseSource(typeof(SellWhenNoDrinkThenErrorTestCaseSource))]
        public void SellWhenNoDrinkThenException(string name, LemonadeVendingMachine machine)
        {
            Assert.Throws<ArgumentException>(() => machine.Sell(name));
        }

        [TestCaseSource(typeof(SellWhenNoIngredientsThenErrorTestCaseSource))]
        public void SellWhenNoIngredientsThenException(string name, LemonadeVendingMachine machine)
        {
            Assert.Throws<ArgumentException>(() => machine.Sell(name));
        }

        [TestCaseSource(typeof(SellWhenMachineBrokenThenErrorTestCaseSource))]
        public void SellWhenMachineBrokenThenException(string name, LemonadeVendingMachine machine)
        {
            Assert.Throws<Exception>(() => machine.Sell(name));
        }

        [TestCaseSource(typeof(LoadTestCaseSource))]
        public void LoadTest(LemonadeVendingMachine machine, LemonadeVendingMachine expected)
        {
            machine.Load();

            var actual = machine;

            Assert.That(actual, Is.EqualTo(expected));
        }

        [TestCaseSource(typeof(SetDrinkTypesStorageTestCaseSource))]
        public void SetDrinkTypesStorageTest(string path, LemonadeVendingMachine machine, LemonadeVendingMachine expected)
        {
            machine.SetDrinkTypesStorage(path);
            var actual = machine;
            Assert.That(actual, Is.EqualTo(expected));
        }
    }
}
