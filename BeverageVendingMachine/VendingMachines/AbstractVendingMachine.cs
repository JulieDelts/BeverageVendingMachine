using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeverageVendingMachine.VendingMachines
{
    public abstract class AbstractVendingMachine
    {
        public int Id { get; init; }

        public int CurrentPurchaseCount { get; protected set; }

        public readonly int MaxPurchaseCountBeforeBreakingDown;

        public AbstractVendingMachine(int id, int maxPurchaseCountBeforeBreakingDown)
        {
            Id = id;
            MaxPurchaseCountBeforeBreakingDown = maxPurchaseCountBeforeBreakingDown;
            CurrentPurchaseCount = 0;
        }

        public void Repair()
        {
            CurrentPurchaseCount = 0;
        }

        public abstract void DisplayAvailableDrinkTypes();

    }
}
