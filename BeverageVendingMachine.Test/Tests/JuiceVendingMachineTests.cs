using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BeverageVendingMachine.DrinkModels;
using BeverageVendingMachineTest.TestSources.JuiceVendingMachineTestCaseSource;
using BeverageVendingMachine.VendingMachines;

namespace BeverageVendingMachineTest.Tests
{
    public class JuiceVendingMachineTests
    {
        [TestCaseSource(typeof(SellTestCaseSource))]
        public void SellTest(string name, JuiceVendingMachine machine, AbstractDrink expected)
        {
            var drink = machine.Sell(name);

            var actual = drink;

            Assert.That(actual, Is.EqualTo(expected));
        }

        [TestCaseSource(typeof(SellWhenNoDrinkThenErrorTestCaseSource))]
        public void SellWhenNoDrinkThenException(string name, JuiceVendingMachine machine)
        {
            Assert.Throws<ArgumentException>(() => machine.Sell(name));
        }

        [TestCaseSource(typeof(SellWhenNoIngredientsThenErrorTestCaseSource))]
        public void SellWhenNoIngredientsThenException(string name, JuiceVendingMachine machine)
        {
            Assert.Throws<ArgumentException>(() => machine.Sell(name));
        }

        [TestCaseSource(typeof(SellWhenMachineBrokenThenErrorTestCaseSource))]
        public void SellWhenMachineBrokenThenException(string name, JuiceVendingMachine machine)
        {
            Assert.Throws<Exception>(() => machine.Sell(name));
        }

        [TestCaseSource(typeof(LoadTestCaseSource))]
        public void LoadTest(JuiceVendingMachine machine, JuiceVendingMachine expected)
        {
            machine.Load();

            var actual = machine;

            Assert.That(actual, Is.EqualTo(expected));
        }

        [TestCaseSource(typeof(SetDrinkTypesStorageTestCaseSource))]
        public void SetDrinkTypesStorageTest(string path, JuiceVendingMachine machine, JuiceVendingMachine expected)
        {
            machine.SetDrinkTypesStorage(path);
            var actual = machine;
            Assert.That(actual, Is.EqualTo(expected));
        }
    }
}
