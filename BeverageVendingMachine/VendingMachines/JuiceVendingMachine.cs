using System;
using BeverageVendingMachine.DrinkModels;

namespace BeverageVendingMachine.VendingMachines
{
    public class JuiceVendingMachine : AbstractVendingMachine<Juice>
    {
        public int CurrentLoad { get; private set; }

        public Dictionary<string, int> FruitAmount { get; private set; }

        public int NumberOfCups { get; private set; }

        public DateTime TimeOfLastUpdate { get; private set; }

        public DateTime InteractionTime { get; set; }

        public const int MaxCapacity = 50;

        public JuiceVendingMachine(int id, int maxPurchaseCountBeforeBreakingDown, DrinkTypesStorage<Juice> drinkTypesStorage):
            base(id, maxPurchaseCountBeforeBreakingDown, drinkTypesStorage)
        {
            FruitAmount = new Dictionary<string, int>();
            CurrentLoad = 0;
            NumberOfCups = 0;
        }

        public override Drink Sell(string drinkType)
        {
            if (IsReadyToSell())
            {
                drinkType = drinkType.ToLower();
                Juice juice = DrinkTypesStorage.GetType(drinkType);

                if (FruitAmount.ContainsKey(drinkType)
                && FruitAmount[drinkType] >= juice.FruitAmountNeeded
                && NumberOfCups > 0)
                {
                    CurrentLoad -= juice.FruitAmountNeeded;
                    FruitAmount[drinkType] -= juice.FruitAmountNeeded;
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

        public override void DisplayAvailableDrinkTypes()
        {
            Console.WriteLine("Available juice types:");

            foreach (string fruit in FruitAmount.Keys)
            {
                Console.WriteLine(fruit);
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

                FruitAmount[drinkType.Key] = FruitAmountPerType;
                CurrentLoad += FruitAmountPerType;
            }

            TimeOfLastUpdate = DateTime.Now;
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

                foreach (string juiceType in FruitAmount.Keys)
                {
                    streamWriter.WriteLine($"{juiceType}: {FruitAmount[juiceType]}");
                }

                streamWriter.WriteLine($"NumberOfCups: {NumberOfCups}");
            }
        }

        protected override bool IsReadyToSell()
        {
            bool readyToSellGoods = true;

            if (CurrentPurchaseCount == MaxPurchaseCountBeforeBreakingDown || TimeOfLastUpdate.AddDays(2) < InteractionTime)
            {
                readyToSellGoods = false;
            }

            return readyToSellGoods;
        }

        public override bool Equals(object? obj)
        {
            return obj is JuiceVendingMachine machine &&
                   Id == machine.Id &&
                   EqualityComparer<DrinkTypesStorage<Juice>>.Default.Equals(DrinkTypesStorage, machine.DrinkTypesStorage) &&
                   MaxPurchaseCountBeforeBreakingDown == machine.MaxPurchaseCountBeforeBreakingDown;
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
    }
}
