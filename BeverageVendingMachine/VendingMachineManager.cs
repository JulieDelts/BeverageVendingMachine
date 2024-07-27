using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BeverageVendingMachine.DrinkModels;
using BeverageVendingMachine.VendingMachines;

namespace BeverageVendingMachine
{
    public class VendingMachineManager
    {
        private List<AbstractVendingMachine> _vendingMachines;

        public VendingMachineManager()
        {
            _vendingMachines = new List<AbstractVendingMachine>();
        }

        public VendingMachineManager(List<AbstractVendingMachine> abstractVendingMachines)
        {
            _vendingMachines = new List<AbstractVendingMachine>(abstractVendingMachines);
        }

        public AbstractVendingMachine GetById(int id)
        {
            AbstractVendingMachine? vendingMachine = null;

            foreach (var machine in _vendingMachines)
            {
                if (machine.Id == id)
                {
                    vendingMachine = machine;
                    break;
                }
            }

            if (vendingMachine is null)
            {
                throw new ArgumentException("The vending machine is not available.");
            }

            return vendingMachine;
        }

        public void Add(AbstractVendingMachine vendingMachine)
        {
            _vendingMachines.Add(vendingMachine); 
        }

        public void Add(List<AbstractVendingMachine> abstractVendingMachines)
        {
            _vendingMachines.AddRange(abstractVendingMachines);
        }

        public void RemoveById(int id)
        {
            AbstractVendingMachine? machine = GetById(id);

            if (machine is not null)
            {
                _vendingMachines.Remove(machine);
            }
        }

        public void LoadAll()
        {
            foreach (var machine in _vendingMachines)
            {
                machine.Load();
            }
        }

        public void RepairAll()
        {
            foreach (var machine in _vendingMachines)
            {
                machine.Repair();
            }
        }

        public void LogDiagnosticsInfoShared(string filePath)
        {
            foreach (var machine in _vendingMachines)
            {
                machine.LogDiagnosticsInfo(filePath);
            }
        }
    }
}
