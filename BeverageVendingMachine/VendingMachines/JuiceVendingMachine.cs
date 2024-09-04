using BeverageVendingMachine.DrinkModels;

namespace BeverageVendingMachine.VendingMachines
{
    public class JuiceVendingMachine : AbstractVendingMachine
    {
        public DrinkTypesStorage<Juice> DrinkTypesStorage { get; private set; }

        public int CurrentLoad { get; private set; }

        public int NumberOfCups { get; private set; }

        public DateTime TimeOfLastUpdate { get; private set; }

        public DateTime InteractionTime { get; set; }

        public const int MaxCapacity = 60;

        private Dictionary<string, int> _fruitAmount;

        public JuiceVendingMachine(int id,
            int maxPurchaseCountBeforeBreakingDown,
            DrinkTypesStorage<Juice> drinkTypesStorage) :
            base(id, maxPurchaseCountBeforeBreakingDown)
        {
            _fruitAmount = new Dictionary<string, int>();
            DrinkTypesStorage = drinkTypesStorage;
            CurrentLoad = 0;
            NumberOfCups = 0;
        }

        public JuiceVendingMachine(int id,
            int maxPurchaseCountBeforeBreakingDown,
            DrinkTypesStorage<Juice> drinkTypesStorage,
            int currentLoad,
            int numberOfCups,
            Dictionary<string, int> fruitAmount):
            base(id, maxPurchaseCountBeforeBreakingDown)
        {
            DrinkTypesStorage = drinkTypesStorage;
            CurrentLoad = currentLoad;
            NumberOfCups = numberOfCups;
            _fruitAmount = fruitAmount;
        }

        public override AbstractDrink Sell(string drinkName)
        {
            if (IsReadyToSell())
            {
                drinkName = drinkName.ToLower();
                Juice juice = DrinkTypesStorage.Get(drinkName);

                if (_fruitAmount.ContainsKey(drinkName)
                && _fruitAmount[drinkName] >= juice.FruitAmountNeeded
                && NumberOfCups > 0)
                {
                    CurrentLoad -= juice.FruitAmountNeeded;
                    _fruitAmount[drinkName] -= juice.FruitAmountNeeded;
                    NumberOfCups--;
                    CurrentPurchaseCount++;

                    return juice;
                }
                else
                {
                    throw new ArgumentException("The drink is not available.");
                }
            }
            else
            {
                throw new Exception("The vending machine is broken or the fruit is rotten.");
            }
        }

        public override void Load()
        {
            NumberOfCups = MaxCapacity;
            Dictionary<string, Juice> drinkTypes = DrinkTypesStorage.GetAllTypes();
            int FruitAmountPerType = MaxCapacity / drinkTypes.Count;

            foreach (var drinkType in drinkTypes)
            {
                if (MaxCapacity - CurrentLoad < FruitAmountPerType)
                {
                    break;
                }

                _fruitAmount[drinkType.Key] = FruitAmountPerType;
                CurrentLoad += FruitAmountPerType;
            }

            TimeOfLastUpdate = DateTime.Now;
        }

        public override void DisplayAvailableDrinkTypes()
        {
            Console.WriteLine("Available juice types:");

            foreach (string fruit in _fruitAmount.Keys)
            {
                Console.WriteLine(fruit);
            }
        }

        public override void LogDiagnosticsInfo(string filePath)
        {
            using (StreamWriter streamWriter = new StreamWriter(filePath, true))
            {
                streamWriter.WriteLine($"Vending machine {Id} diagnostics info [{InteractionTime}]:");

                Juice juice = new Juice();

                if (CurrentPurchaseCount < MaxPurchaseCountBeforeBreakingDown)
                {
                    streamWriter.WriteLine("The machine is functioning properly.");
                }
                else
                {
                    streamWriter.WriteLine("The machine is worn down.");
                }

                if (TimeOfLastUpdate.AddDays(2) > InteractionTime)
                {
                    streamWriter.WriteLine($"The fruit is still fresh. The last load time: {TimeOfLastUpdate}. The time of interaction with the machine: {InteractionTime}.");
                }
                else
                {
                    streamWriter.WriteLine($"The fruit is rotten. The last load time: {TimeOfLastUpdate}. The time of interaction with the machine: {InteractionTime}.");
                }

                streamWriter.WriteLine("Current load:");

                foreach (string juiceType in _fruitAmount.Keys)
                {
                    streamWriter.WriteLine($"{juiceType}: {_fruitAmount[juiceType]}");
                }

                streamWriter.WriteLine($"NumberOfCups: {NumberOfCups}");
            }
        }

        public void SetDrinkTypesStorage(string path)
        {
            if (Path.Exists(path))
            {
                DrinkTypesStorage = new DrinkTypesStorage<Juice>(path);
            }
        }

        protected override bool IsReadyToSell()
        {
            return CurrentPurchaseCount == MaxPurchaseCountBeforeBreakingDown || TimeOfLastUpdate.AddDays(2) < InteractionTime;
        }

        public override bool Equals(object? obj)
        {
            var machine = obj as JuiceVendingMachine;

            if (machine is null)
            {
                return false;
            }

            if (!(Id == machine.Id &&
                  CurrentPurchaseCount == machine.CurrentPurchaseCount &&
                  MaxPurchaseCountBeforeBreakingDown == machine.MaxPurchaseCountBeforeBreakingDown &&
                  EqualityComparer<DrinkTypesStorage<Juice>>.Default.Equals(DrinkTypesStorage, machine.DrinkTypesStorage) &&
                  CurrentLoad == machine.CurrentLoad &&
                  NumberOfCups == machine.NumberOfCups))
            {
                return false;
            }

            if (_fruitAmount.Count != machine._fruitAmount.Count || !_fruitAmount.OrderBy(kvp => kvp.Key).SequenceEqual(machine._fruitAmount.OrderBy(kvp => kvp.Key)))
            { 
                return false;
            }

            return true;
        }
   
        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
    }
}
