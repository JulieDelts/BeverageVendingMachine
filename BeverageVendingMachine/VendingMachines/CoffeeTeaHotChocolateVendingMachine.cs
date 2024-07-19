using BeverageVendingMachine.DrinkModels;
using BeverageVendingMachine.StorageUnits;

namespace BeverageVendingMachine.VendingMachines
{
    public class CoffeeTeaHotChocolateVendingMachine : CoffeeVendingMachine
    {
        public double AmountOfCocoaPowder { get; private set; }

        public int NumberOfTeaBags { get; private set; }

        public TeaTypesStorage TeaTypesStorageUnit { get; private set; }

        public const double MaxCocoaCapacity = 500;

        public const int MaxNumberOfTeaBags = 40;

        private const int _maxPurchaseCountBeforeBreakingDown = 15;

        private int _currentPurchaseCount;

        public CoffeeTeaHotChocolateVendingMachine(int id, CoffeeTypesStorage coffeeTypesStorage, TeaTypesStorage teaTypesStorage): base(id, coffeeTypesStorage)
        {
            Id = id;
            AmountOfCocoaPowder = 0;
            NumberOfTeaBags = 0;
            TeaTypesStorageUnit = teaTypesStorage;
            _currentPurchaseCount = 0;
        }

        public Tea? SellTea(string teaName)
        {
            bool isReadyToSell = IsReadyToSell();

            if (isReadyToSell)
            {
                Dictionary<string, Tea> teaTypes = TeaTypesStorageUnit.GetAllTeaTypes();

                if (teaTypes.ContainsKey(teaName))
                {
                    Tea tea = teaTypes[teaName];

                    if (NumberOfCups > 0
                        && NumberOfTeaBags > 0
                        && AmountOfSugar >= tea.Sugar
                        && AmountOfWater >= tea.Water)
                    {
                        NumberOfCups--;
                        NumberOfTeaBags--;
                        AmountOfSugar -= tea.Sugar;
                        AmountOfWater -= tea.Water;
                        _currentPurchaseCount++;

                        return tea;
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

        public HotChocolate? SellHotChocolate()
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
                    _currentPurchaseCount++;

                    return hotChocolate;
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
            Dictionary<string, Coffee> coffeeTypes = CoffeeTypesStorageUnit.GetAllCoffeeTypes();

            Console.WriteLine("Available coffee types:");

            foreach (string coffee in coffeeTypes.Keys)
            {
                Console.WriteLine(coffee);
            }

            Dictionary<string, Tea> teaTypes = TeaTypesStorageUnit.GetAllTeaTypes();

            Console.WriteLine("Available tea types:");

            foreach (string tea in teaTypes.Keys)
            {
                Console.WriteLine(tea);
            }

            Console.WriteLine("Hot chocolate");
        }

        public override void ShowDiagnosticsInfo()
        {
            base.ShowDiagnosticsInfo();
            Console.WriteLine($"Amount of cocoa powder: {AmountOfCocoaPowder}");
            Console.WriteLine($"Number of tea bags: {NumberOfTeaBags}");
        }

        public override void Load()
        {
            NumberOfCups = MaxNumberOfCups;
            AmountOfCoffeePowder = MaxCoffeePowderCapacity;
            AmountOfMilkPowder = MaxMilkCapacity;
            AmountOfSugar = MaxSugarCapacity;
            AmountOfWater = MaxWaterCapacity;
            AmountOfCocoaPowder = MaxCocoaCapacity;
            NumberOfTeaBags = MaxNumberOfTeaBags;
        }
    }
}
