using BeverageVendingMachine.DrinkModels;

namespace BeverageVendingMachine.VendingMachines
{
    public class LemonadeVendingMachine: AbstractVendingMachine
    {
        public DrinkTypesStorage<Lemonade> DrinkTypesStorage { get; private set; }

        public int CurrentLoad { get; private set; }

        public const int MaxCapacity = 60;

        private Dictionary<string, int> _lemonadeCansNumber;

        public LemonadeVendingMachine(int id, int maxPurchaseCountBeforeBreakingDown, DrinkTypesStorage<Lemonade> drinkTypesStorage) :
            base(id, maxPurchaseCountBeforeBreakingDown)
        {
            _lemonadeCansNumber = new Dictionary<string, int>();
            DrinkTypesStorage = drinkTypesStorage;
            CurrentLoad = 0;
        }

        public override AbstractDrink Sell(string drinkName)
        {
            if (IsReadyToSell())
            {
                drinkName = drinkName.ToLower();

                if (_lemonadeCansNumber.ContainsKey(drinkName) 
                    && _lemonadeCansNumber[drinkName] >= 1)
                {
                    Lemonade lemonade = DrinkTypesStorage.GetType(drinkName);
                    CurrentLoad--;
                    _lemonadeCansNumber[drinkName]--;
                    CurrentPurchaseCount++;

                    return lemonade;
                }
                else
                {
                    throw new ArgumentException("The drink is not available.");
                }
            }
            else
            {
                throw new Exception("The vending machine is broken.");
            }
        }

        public override void Load()
        {
            Dictionary<string, Lemonade> drinkTypes = DrinkTypesStorage.GetAllTypes();
            int numberOfCansPerType = MaxCapacity / drinkTypes.Count;

            foreach (var drinkType in drinkTypes)
            {
                if (MaxCapacity - CurrentLoad < numberOfCansPerType)
                {
                    break;
                }

                _lemonadeCansNumber[drinkType.Key] = numberOfCansPerType;
                CurrentLoad += numberOfCansPerType;
            }
        }

        public override void DisplayAvailableDrinkTypes()
        {
            Console.WriteLine("Available lemonade types:");

            foreach (string lemonade in _lemonadeCansNumber.Keys)
            {
                Console.WriteLine(lemonade);
            }
        }

        public override void LogDiagnosticsInfo(string filePath)
        {
            using (StreamWriter streamWriter = new StreamWriter(filePath, true))
            {
                streamWriter.WriteLine($"Vending machine {Id} diagnostics info [{DateTime.Now}]:");

                if (CurrentPurchaseCount < MaxPurchaseCountBeforeBreakingDown)
                {
                    streamWriter.WriteLine("The machine is functioning properly.");
                }
                else
                {
                    streamWriter.WriteLine("The machine is worn down.");
                }

                streamWriter.WriteLine("Current load:");

                foreach (string lemonadeType in _lemonadeCansNumber.Keys)
                {
                    streamWriter.WriteLine($"{lemonadeType}: {_lemonadeCansNumber[lemonadeType]}");
                }
            }
        }

        public void SetDrinkTypesStorage(string path)
        {
            if (Path.Exists(path))
            {
                DrinkTypesStorage = new DrinkTypesStorage<Lemonade>(path);
            }
        }

        public override bool Equals(object? obj)
        {
            return obj is LemonadeVendingMachine machine &&
                   Id == machine.Id &&
                   EqualityComparer<DrinkTypesStorage<Lemonade>>.Default.Equals(DrinkTypesStorage, machine.DrinkTypesStorage) &&
                   MaxPurchaseCountBeforeBreakingDown == machine.MaxPurchaseCountBeforeBreakingDown;
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
    }
}
