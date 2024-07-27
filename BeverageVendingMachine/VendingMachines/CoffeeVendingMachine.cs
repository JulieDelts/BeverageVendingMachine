using BeverageVendingMachine.DrinkModels;

namespace BeverageVendingMachine.VendingMachines
{
    public class CoffeeVendingMachine: AbstractVendingMachine
    {
        public DrinkTypesStorage<Coffee> DrinkTypesStorage { get; protected set; }

        public double AmountOfCoffeePowder { get; protected set; }

        public double AmountOfMilkPowder { get; protected set; }

        public double AmountOfSugar { get; protected set; }

        public double AmountOfWater { get; protected set; }

        public int NumberOfCups { get; protected set; }

        public const int MaxNumberOfCups = 40;

        public const double MaxCoffeePowderCapacity = 500;

        public const double MaxMilkCapacity = 1000;

        public const double MaxSugarCapacity = 500;

        public const double MaxWaterCapacity = 2000;

        public CoffeeVendingMachine(int id, int maxPurchaseCountBeforeBreakingDown, DrinkTypesStorage<Coffee> drinkTypesStorage) :
            base(id, maxPurchaseCountBeforeBreakingDown)
        {
            DrinkTypesStorage = drinkTypesStorage;
            NumberOfCups = 0;
            AmountOfCoffeePowder = 0;
            AmountOfMilkPowder = 0;
            AmountOfSugar = 0;
            AmountOfWater = 0;
        }

        public void SetDrinkTypesStorage(string path)
        {
            if (Path.Exists(path))
            {
                DrinkTypesStorage = new DrinkTypesStorage<Coffee>(path);
            }
        }

        public override AbstractDrink Sell(string drinkName)
        {
            drinkName = drinkName.ToLower();

            bool isReadyToSell = IsReadyToSell();

            if (isReadyToSell)
            {
                Coffee coffee = DrinkTypesStorage.GetType(drinkName);

                if (NumberOfCups > 0
                    && AmountOfCoffeePowder >= coffee.CoffeePowder
                    && AmountOfMilkPowder >= coffee.MilkPowder
                    && AmountOfSugar >= coffee.Sugar
                    && AmountOfWater >= coffee.Water)
                {
                    NumberOfCups--;
                    AmountOfWater -= coffee.Water;
                    AmountOfCoffeePowder -= coffee.CoffeePowder;
                    AmountOfMilkPowder -= coffee.MilkPowder;
                    AmountOfSugar -= coffee.Sugar;
                    CurrentPurchaseCount++;

                    return coffee;
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
            NumberOfCups = MaxNumberOfCups;
            AmountOfCoffeePowder = MaxCoffeePowderCapacity;
            AmountOfMilkPowder = MaxMilkCapacity;
            AmountOfSugar = MaxSugarCapacity;
            AmountOfWater = MaxWaterCapacity;
        }

        public override void DisplayAvailableDrinkTypes()
        {
            Dictionary<string, Coffee> coffeeTypes = DrinkTypesStorage.GetAllTypes();

            Console.WriteLine("Available coffee types:");

            foreach (string coffee in coffeeTypes.Keys)
            {
                Console.WriteLine(coffee);
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
                streamWriter.WriteLine($"Number of cups: {NumberOfCups}");
                streamWriter.WriteLine($"Amount of coffee powder: {AmountOfCoffeePowder}");
                streamWriter.WriteLine($"Amount of milk powder: {AmountOfMilkPowder}");
                streamWriter.WriteLine($"Amount of water: {AmountOfWater}");
                streamWriter.WriteLine($"Amount of sugar: {AmountOfSugar}");
            }
        }

        public override bool Equals(object? obj)
        {
            return obj is CoffeeVendingMachine machine &&
                   Id == machine.Id &&
                   EqualityComparer<DrinkTypesStorage<Coffee>>.Default.Equals(DrinkTypesStorage, machine.DrinkTypesStorage) &&
                   MaxPurchaseCountBeforeBreakingDown == machine.MaxPurchaseCountBeforeBreakingDown;
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
    }
}
