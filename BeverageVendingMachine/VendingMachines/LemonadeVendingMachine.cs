using BeverageVendingMachine.DrinkModels;
using BeverageVendingMachine.StorageUnits;

namespace BeverageVendingMachine.VendingMachines
{
    public class LemonadeVendingMachine
    {
        public int Id { get; init; }

        public LemonadeTypesStorage TypesStorageUnit { get; private set; }

        public Dictionary<string, int> LemonadeCansNumber { get; private set; }

        public int CurrentLoad { get; private set; }

        public const int MaxCapacity = 60;

        private const int _maxPurchaseCountBeforeBreakingDown = 20;

        private int _currentPurchaseCount;

        public LemonadeVendingMachine(int id, LemonadeTypesStorage typesStorage)
        {
            Id = id;
            TypesStorageUnit = typesStorage;
            LemonadeCansNumber = new Dictionary<string, int>();
            CurrentLoad = 0;
            _currentPurchaseCount = 0;
        }

        public Lemonade? Sell(string lemonadeType)
        {
            lemonadeType = lemonadeType.ToLower();

            bool readyToSell = IsReadyToSell();

            if (readyToSell)
            {
                if (LemonadeCansNumber.ContainsKey(lemonadeType) && LemonadeCansNumber[lemonadeType] >= 1)
                {
                    Lemonade lemonade = TypesStorageUnit.GetLemonadeType(lemonadeType);
                    CurrentLoad--;
                    LemonadeCansNumber[lemonadeType]--;
                    _currentPurchaseCount++;

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
            Dictionary<string, Lemonade> lemonadeTypes = TypesStorageUnit.GetAllLemonadeTypes();

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

        public void DisplayAvailableDrinkTypes()
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

            if (_currentPurchaseCount < _maxPurchaseCountBeforeBreakingDown)
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

        public void Repair()
        {
            _currentPurchaseCount = 0;
        }

        private bool IsReadyToSell()
        {
            bool readyToSellGoods = true;

            if (_currentPurchaseCount == _maxPurchaseCountBeforeBreakingDown)
            {
                readyToSellGoods = false;
            }

            return readyToSellGoods;
        }
    }
}
