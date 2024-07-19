using BeverageVendingMachine.DrinkModels;
using BeverageVendingMachine.StorageUnits;

namespace BeverageVendingMachine.VendingMachines
{
    public class LemonadeVendingMachine: AbstractVendingMachine
    {
        public LemonadeTypesStorage TypesStorageUnit { get; private set; }

        public Dictionary<string, int> LemonadeCansNumber { get; private set; }

        public int CurrentLoad { get; private set; }

        public const int MaxCapacity = 60;

        public LemonadeVendingMachine(int id, int maxPurchaseCountBeforeBreakingDown, LemonadeTypesStorage typesStorage): base(id, maxPurchaseCountBeforeBreakingDown)
        {
            TypesStorageUnit = typesStorage;
            LemonadeCansNumber = new Dictionary<string, int>();
            CurrentLoad = 0;
        }

        public Lemonade? Sell(string lemonadeType)
        {
            lemonadeType = lemonadeType.ToLower();

            bool readyToSell = IsReadyToSell();

            if (readyToSell)
            {
                if (LemonadeCansNumber.ContainsKey(lemonadeType) && LemonadeCansNumber[lemonadeType] >= 1)
                {
                    Lemonade lemonade = TypesStorageUnit.GetType(lemonadeType);
                    CurrentLoad--;
                    LemonadeCansNumber[lemonadeType]--;
                    CurrentPurchaseCount++;

                    return lemonade;
                }
                else
                {
                    return null;
                }
            }
            else
            {
                return null;
            }
        }

        public void Load(string lemonadeType, int numberOfCans)
        {
            Dictionary<string, Lemonade> lemonadeTypes = TypesStorageUnit.GetAllTypes();

            lemonadeType = lemonadeType.ToLower();

            if (lemonadeTypes.ContainsKey(lemonadeType))
            {
                if (numberOfCans + CurrentLoad <= MaxCapacity)
                {
                    if (LemonadeCansNumber.ContainsKey(lemonadeType))
                    {
                        LemonadeCansNumber[lemonadeType] += numberOfCans;
                    }
                    else
                    {
                        LemonadeCansNumber[lemonadeType] = numberOfCans;
                    }

                    CurrentLoad += numberOfCans;
                }
                else
                {
                    if (LemonadeCansNumber.ContainsKey(lemonadeType))
                    {
                        LemonadeCansNumber[lemonadeType] += MaxCapacity - CurrentLoad;
                    }
                    else
                    {
                        LemonadeCansNumber[lemonadeType] = MaxCapacity - CurrentLoad;
                    }
                    
                    CurrentLoad = MaxCapacity;
                }
            }
        }

        public override void DisplayAvailableDrinkTypes()
        {
            Console.WriteLine("Available lemonade types:");

            foreach (string lemonade in LemonadeCansNumber.Keys)
            {
                Console.WriteLine(lemonade);
            }
        }

        public void ShowDiagnosticsInfo()
        {
            Console.WriteLine($"Vending machine {Id} diagnostics info:");

            if (CurrentPurchaseCount < MaxPurchaseCountBeforeBreakingDown)
            {
                Console.WriteLine("The machine is functioning properly.");
            }
            else
            {
                Console.WriteLine("The machine is worn down.");
            }

            Console.WriteLine("Current load:");

            foreach (string lemonadeType in LemonadeCansNumber.Keys)
            {
                Console.WriteLine($"{lemonadeType}: {LemonadeCansNumber[lemonadeType]}");
            }
        }

        private bool IsReadyToSell()
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
