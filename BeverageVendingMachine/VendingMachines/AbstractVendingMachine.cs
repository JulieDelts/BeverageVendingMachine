using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BeverageVendingMachine.DrinkModels;

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

        public abstract AbstractDrink Sell(string drinkName);

        public void Repair()
        {
            CurrentPurchaseCount = 0;
        }

        public abstract void Load();

        public abstract void DisplayAvailableDrinkTypes();

        public abstract void LogDiagnosticsInfo(string filePath);

        public override string ToString()
        {
            return $"Drink vending machine, type {GetType()}, ID {Id}";
        }

        protected virtual bool IsReadyToSell()
        {
            bool readyToSellGoods = true;

            if (CurrentPurchaseCount == MaxPurchaseCountBeforeBreakingDown)
            {
                readyToSellGoods = false;
            }

            return readyToSellGoods;
        }

    }
}
