using BeverageVendingMachine.DrinkModels;

namespace BeverageVendingMachine.VendingMachines
{
    public class CoffeeTeaHotChocolateVendingMachine : CoffeeVendingMachine
    {
        public DrinkTypesStorage<Tea> AdditionalDrinkTypesStorage { get; private set; }

        public double AmountOfCocoaPowder { get; private set; }

        public int NumberOfTeaBags { get; private set; }

        public const double MaxCocoaCapacity = 500;

        public const int MaxNumberOfTeaBags = 40;

        public CoffeeTeaHotChocolateVendingMachine(int id,
            DrinkTypesStorage<Coffee> drinkTypesStorage,
            int maxPurchaseCountBeforeBreakingDown,
            DrinkTypesStorage<Tea> teaTypesStorage) : base(id, maxPurchaseCountBeforeBreakingDown, drinkTypesStorage)
        {
            AmountOfCocoaPowder = 0;
            NumberOfTeaBags = 0;
            AdditionalDrinkTypesStorage = teaTypesStorage;
        }

        public CoffeeTeaHotChocolateVendingMachine(int id,
            DrinkTypesStorage<Coffee> drinkTypesStorage,
            int maxPurchaseCountBeforeBreakingDown,
            DrinkTypesStorage<Tea> additionalDrinkTypesStorage,
            double amountOfCocoaPowder,
            double amountOfMilkPowder,
            double amountOfCoffeePowder,
            double amountOfSugar,
            double amountOfWater,
            int numberOfCups,
            int numberOfTeaBags):
            base(id,
                maxPurchaseCountBeforeBreakingDown,
                drinkTypesStorage,
                amountOfCoffeePowder, 
                amountOfMilkPowder,
                amountOfSugar,
                amountOfWater,
                numberOfCups )
        {
            AdditionalDrinkTypesStorage = additionalDrinkTypesStorage;
            AmountOfCocoaPowder = amountOfCocoaPowder;
            NumberOfTeaBags = numberOfTeaBags;
        }

        public override AbstractDrink Sell(string drinkName)
        {
            drinkName = drinkName.ToLower();
            Dictionary<string, Coffee> coffeeTypes = DrinkTypesStorage.GetAllTypes();
            Dictionary<string, Tea> teaTypes = AdditionalDrinkTypesStorage.GetAllTypes();

            AbstractDrink drink;

            if (coffeeTypes.ContainsKey(drinkName))
            {
                drink = base.Sell(drinkName);
            }
            else if (teaTypes.ContainsKey(drinkName))
            {
                drink = SellTea(drinkName);
            }
            else if (drinkName == "hot chocolate")
            {
                drink = SellHotChocolate();
            }
            else
            {
                throw new ArgumentException("The drink is not present is the storages.");
            }

            return drink;
        }

        public override void DisplayAvailableDrinkTypes()
        {
            Dictionary<string, Coffee> coffeeTypes = DrinkTypesStorage.GetAllTypes();

            Console.WriteLine("Available coffee types:");

            foreach (string coffee in coffeeTypes.Keys)
            {
                Console.WriteLine(coffee);
            }

            Dictionary<string, Tea> teaTypes = AdditionalDrinkTypesStorage.GetAllTypes();

            Console.WriteLine("Available tea types:");

            foreach (string tea in teaTypes.Keys)
            {
                Console.WriteLine(tea);
            }

            Console.WriteLine("Hot chocolate");
        }

        public override void LogDiagnosticsInfo(string filePath)
        {
            base.LogDiagnosticsInfo(filePath);
            using (StreamWriter streamWriter = new StreamWriter(filePath, true))
            {
                streamWriter.WriteLine($"Amount of cocoa powder: {AmountOfCocoaPowder}");
                streamWriter.WriteLine($"Number of tea bags: {NumberOfTeaBags}");
            }
        }

        public override void Load()
        {
            base.Load();
            AmountOfCocoaPowder = MaxCocoaCapacity;
            NumberOfTeaBags = MaxNumberOfTeaBags;
        }

        public void SetAdditionalDrinkTypesStorage(string filePath)
        {
            if (Path.Exists(filePath))
            {
                AdditionalDrinkTypesStorage = new DrinkTypesStorage<Tea>(filePath);
            }
        }

        public override bool Equals(object? obj)
        {
            return obj is CoffeeTeaHotChocolateVendingMachine machine &&
                   Id == machine.Id &&
                   CurrentPurchaseCount == machine.CurrentPurchaseCount &&
                   MaxPurchaseCountBeforeBreakingDown == machine.MaxPurchaseCountBeforeBreakingDown &&
                   EqualityComparer<DrinkTypesStorage<Coffee>>.Default.Equals(DrinkTypesStorage, machine.DrinkTypesStorage) &&
                   AmountOfCoffeePowder == machine.AmountOfCoffeePowder &&
                   AmountOfMilkPowder == machine.AmountOfMilkPowder &&
                   AmountOfSugar == machine.AmountOfSugar &&
                   AmountOfWater == machine.AmountOfWater &&
                   NumberOfCups == machine.NumberOfCups &&
                   EqualityComparer<DrinkTypesStorage<Tea>>.Default.Equals(AdditionalDrinkTypesStorage, machine.AdditionalDrinkTypesStorage) &&
                   AmountOfCocoaPowder == machine.AmountOfCocoaPowder &&
                   NumberOfTeaBags == machine.NumberOfTeaBags;
        }
        
        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
        
        private HotChocolate SellHotChocolate()
        {
            bool isReadyToSell = IsReadyToSell();

            if (isReadyToSell)
            {
                HotChocolate hotChocolate = new HotChocolate();

                if (NumberOfCups > 0
                    && AmountOfCocoaPowder >= hotChocolate.CocoaPowder
                    && AmountOfMilkPowder >= hotChocolate.MilkPowder
                    && AmountOfSugar >= hotChocolate.Sugar
                    && AmountOfWater >= hotChocolate.Water)
                {
                    NumberOfCups--;
                    AmountOfWater -= hotChocolate.Water;
                    AmountOfCocoaPowder -= hotChocolate.CocoaPowder;
                    AmountOfMilkPowder -= hotChocolate.MilkPowder;
                    AmountOfSugar -= hotChocolate.Sugar;
                    CurrentPurchaseCount++;

                    return hotChocolate;
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

        private Tea SellTea(string teaName)
        {
            bool isReadyToSell = IsReadyToSell();

            if (isReadyToSell)
            {
                Tea tea = AdditionalDrinkTypesStorage.Get(teaName);

                if (NumberOfCups > 0
                    && NumberOfTeaBags > 0
                    && AmountOfSugar >= tea.Sugar
                    && AmountOfWater >= tea.Water)
                {
                    NumberOfCups--;
                    NumberOfTeaBags--;
                    AmountOfSugar -= tea.Sugar;
                    AmountOfWater -= tea.Water;
                    CurrentPurchaseCount++;

                    return tea;
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

    }
}
