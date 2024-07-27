using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BeverageVendingMachine.DrinkModels;

namespace BeverageVendingMachine.VendingMachines
{
    public abstract class AbstractVendingMachine<T> where T : Drink
    {
        public int Id { get; init; }

        public DrinkTypesStorage<T> DrinkTypesStorage { get; protected set; }

        public int CurrentPurchaseCount { get; protected set; }

        public readonly int MaxPurchaseCountBeforeBreakingDown;

        public AbstractVendingMachine(int id, int maxPurchaseCountBeforeBreakingDown, DrinkTypesStorage<T> drinkTypesStorage)
        {
            Id = id;
            MaxPurchaseCountBeforeBreakingDown = maxPurchaseCountBeforeBreakingDown;
            CurrentPurchaseCount = 0;
            DrinkTypesStorage = drinkTypesStorage;
        }

        public void Repair()
        {
            CurrentPurchaseCount = 0;
        }

        public void SetDrinkTypesStorage(string path)
        {
            if (Path.Exists(path))
            {
                DrinkTypesStorage = new DrinkTypesStorage<T>(path);
            }
        }

        public abstract Drink Sell(string type);

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
