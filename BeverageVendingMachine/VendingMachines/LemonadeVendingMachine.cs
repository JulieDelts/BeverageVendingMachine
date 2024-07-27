using BeverageVendingMachine.DrinkModels;

namespace BeverageVendingMachine.VendingMachines
{
    public class LemonadeVendingMachine : AbstractVendingMachine<Lemonade>
    {
        public Dictionary<string, int> LemonadeCansNumber { get; private set; }

        public int CurrentLoad { get; private set; }

        public const int MaxCapacity = 60;

        public LemonadeVendingMachine(int id, int maxPurchaseCountBeforeBreakingDown, DrinkTypesStorage<Lemonade> drinkTypesStorage) :
            base(id, maxPurchaseCountBeforeBreakingDown, drinkTypesStorage)
        {
            LemonadeCansNumber = new Dictionary<string, int>();
            CurrentLoad = 0;
        }

        public override Drink Sell(string drinkType)
        {
            if (IsReadyToSell())
            {
                drinkType = drinkType.ToLower();

                if (LemonadeCansNumber.ContainsKey(drinkType) 
                    && LemonadeCansNumber[drinkType] >= 1)
                {
                    Lemonade lemonade = DrinkTypesStorage.GetType(drinkType);
                    CurrentLoad--;
                    LemonadeCansNumber[drinkType]--;
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

                LemonadeCansNumber[drinkType.Key] = numberOfCansPerType;
                CurrentLoad += numberOfCansPerType;
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

                foreach (string lemonadeType in LemonadeCansNumber.Keys)
                {
                    streamWriter.WriteLine($"{lemonadeType}: {LemonadeCansNumber[lemonadeType]}");
                }
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
