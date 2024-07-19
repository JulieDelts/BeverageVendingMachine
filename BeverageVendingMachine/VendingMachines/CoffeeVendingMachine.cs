using BeverageVendingMachine.DrinkModels;
using BeverageVendingMachine.StorageUnits;

namespace BeverageVendingMachine.VendingMachines
{
    public class CoffeeVendingMachine : AbstractVendingMachine
    {
        public int NumberOfCups { get; protected set; }

        public double AmountOfCoffeePowder { get; protected set; }

        public double AmountOfMilkPowder { get; protected set; }

        public double AmountOfSugar { get; protected set; }

        public double AmountOfWater { get; protected set; }

        public CoffeeTypesStorage CoffeeTypesStorageUnit { get; protected set; }

        public const int MaxNumberOfCups = 40;

        public const double MaxCoffeePowderCapacity = 500;

        public const double MaxMilkCapacity = 1000;

        public const double MaxSugarCapacity = 500;

        public const double MaxWaterCapacity = 2000;

        public CoffeeVendingMachine(int id, int maxPurchaseCountBeforeBreakingDown, CoffeeTypesStorage typesStorage) : base(id, maxPurchaseCountBeforeBreakingDown)
        {
            NumberOfCups = 0;
            AmountOfCoffeePowder = 0;
            AmountOfMilkPowder = 0;
            AmountOfSugar = 0;
            AmountOfWater = 0;
            CoffeeTypesStorageUnit = typesStorage;
        }

        public Coffee? SellCoffee(string coffeeType)
        {
            coffeeType = coffeeType.ToLower();

            bool isReadyToSell = IsReadyToSell();

            if (isReadyToSell)
            {
                Dictionary<string, Coffee> coffeeTypes = CoffeeTypesStorageUnit.GetAllTypes();

                if (coffeeTypes.ContainsKey(coffeeType))
                {
                    Coffee coffee = coffeeTypes[coffeeType];

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
                        return null;
                    }
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

        public override void DisplayAvailableDrinkTypes()
        {
            Dictionary<string, Coffee> coffeeTypes = CoffeeTypesStorageUnit.GetAllTypes();

            Console.WriteLine("Available coffee types:");

            foreach (string coffee in coffeeTypes.Keys)
            {
                Console.WriteLine(coffee);
            }
        }

        public virtual void ShowDiagnosticsInfo()
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
            Console.WriteLine($"Number of cups: {NumberOfCups}");
            Console.WriteLine($"Amount of coffee powder: {AmountOfCoffeePowder}");
            Console.WriteLine($"Amount of milk powder: {AmountOfMilkPowder}");
            Console.WriteLine($"Amount of water: {AmountOfWater}");
            Console.WriteLine($"Amount of sugar: {AmountOfSugar}");
        }

        public virtual void Load()
        {
            NumberOfCups = MaxNumberOfCups;
            AmountOfCoffeePowder = MaxCoffeePowderCapacity;
            AmountOfMilkPowder = MaxMilkCapacity;
            AmountOfSugar = MaxSugarCapacity;
            AmountOfWater = MaxWaterCapacity;
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
