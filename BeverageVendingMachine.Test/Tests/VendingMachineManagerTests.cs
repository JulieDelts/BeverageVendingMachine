using System.Collections;
using System.ComponentModel.DataAnnotations;
using BeverageVendingMachine.DrinkModels;
using BeverageVendingMachine;
using BeverageVendingMachine.VendingMachines;
using BeverageVendingMachineTest.TestSources.VendingMachineManagerTestCaseSource;

namespace BeverageVendingMachineTest.Tests
{
    public class VendingMachineManagerTests
    {
        [TestCaseSource(typeof(GetTestCaseSource))]
        public void GetTest(int id, VendingMachineManager manager, AbstractVendingMachine expected)
        {
            var machine = manager.Get(id);

            var actual = machine;

            Assert.That(actual, Is.EqualTo(expected));
        }

        [TestCaseSource(typeof(GetWhenNoMachineThenExceptionTestCaseSource))]
        public void GetWhenNoMachineThenException(int id, VendingMachineManager manager)
        {
            Assert.Throws<ArgumentException>(() => manager.Get(id));
        }

        [TestCaseSource(typeof(AddTestCaseSource))]
        public void AddTest(AbstractVendingMachine vendingMachine, VendingMachineManager manager, VendingMachineManager expected)
        {
            manager.Add(vendingMachine);

            var actual = manager;

            Assert.That(actual, Is.EqualTo(expected));
        }

        [TestCaseSource(typeof(AddRangeTestCaseSource))]
        public void AddListTest(List<AbstractVendingMachine> abstractVendingMachines, VendingMachineManager manager, VendingMachineManager expected)
        {
            manager.Add(abstractVendingMachines);

            var actual = manager;

            Assert.That(actual, Is.EqualTo(expected));
        }

        [TestCaseSource(typeof(RemoveTestCaseSource))]
        public void RemoveTest(int id, VendingMachineManager manager, VendingMachineManager expected)
        {
            manager.RemoveById(id);

            var actual = manager;

            Assert.That(actual, Is.EqualTo(expected));
        }

        [TestCaseSource(typeof(LoadAllTestCaseSource))]
        public void LoadAllTest(VendingMachineManager manager, VendingMachineManager expected)
        {
            manager.LoadAll();
            var actual = manager;

            Assert.That(actual, Is.EqualTo(expected));
        }

        [TestCaseSource(typeof(RepairAllTestCaseSource))]
        public void RepairAllTest(VendingMachineManager manager, VendingMachineManager expected)
        {
            manager.RepairAll();
            var actual = manager;

            Assert.That(actual, Is.EqualTo(expected));
        }
    }
}