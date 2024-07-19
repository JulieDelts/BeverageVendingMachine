using BeverageVendingMachine.DrinkModels;

namespace BeverageVendingMachine.VendingMachines
{
    public class OrangeJuiceVendingMachine
    {
        public int Id { get; init; }

        public int CurrentLoad { get; private set; }

        public int NumberOfCups { get; private set; }

        public DateTime TimeOfLastUpdate { get; private set; }

        public const int MaxCapacity = 35;

        private const int _maxPurchaseCountBeforeBreakingDown = 15;

        private int _currentPurchaseCount;

        public OrangeJuiceVendingMachine(int id)
        {
            Id = id;
            CurrentLoad = 0;
            NumberOfCups = 0;
            _currentPurchaseCount = 0;
        }

        public OrangeJuice? Sell(DateTime timeOfPurchase)
        {
            bool readyToSell = IsReadyToSell(timeOfPurchase);

            if (readyToSell)
            {
                OrangeJuice orangeJuice = new OrangeJuice();

                if (CurrentLoad >= orangeJuice.NumberOfOrangesNeeded
                    && NumberOfCups > 0)
                {
                    CurrentLoad -= orangeJuice.NumberOfOrangesNeeded;
                    NumberOfCups--;
                    _currentPurchaseCount++;

                    return orangeJuice;
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

        public void Repair()
        {
            _currentPurchaseCount = 0;
        }

        public void Load()
        {
            CurrentLoad = MaxCapacity;
            NumberOfCups = MaxCapacity;
            TimeOfLastUpdate = DateTime.Now;
        }

        public void ShowDiagnosticsInfo(DateTime dateTime)
        {
            Console.WriteLine($"Vending machine {Id} diagnostics info:");

            OrangeJuice orangeJuice = new OrangeJuice();

            if (_currentPurchaseCount < _maxPurchaseCountBeforeBreakingDown)
            {
                Console.WriteLine("The machine is functioning properly.");
            }
            else
            {
                Console.WriteLine("The machine is worn down.");
            }

            if (TimeOfLastUpdate.AddDays(2) > dateTime)
            {
                Console.WriteLine($"The fruit is still fresh. The last load time: {TimeOfLastUpdate}.");
            }
            else
            {
                Console.WriteLine($"The fruit is rotten. The last load time: {TimeOfLastUpdate}.");
            }

            if (CurrentLoad > orangeJuice.NumberOfOrangesNeeded && NumberOfCups > 0)
            {
                Console.WriteLine($"There are still enough ingredients to make some juice. The number of cups: {NumberOfCups}. The current load of oranges: {CurrentLoad}.");
            }
            else
            {
                Console.WriteLine($"There are not enough ingredients to make juice. The number of cups: {NumberOfCups}. The current load of oranges: {CurrentLoad}.");
            }
        }

        private bool IsReadyToSell(DateTime timeOfPurchase)
        {
            bool readyToSellGoods = true;

            if (_currentPurchaseCount == _maxPurchaseCountBeforeBreakingDown || TimeOfLastUpdate.AddDays(2) < timeOfPurchase)
            {
                readyToSellGoods = false;
            }

            return readyToSellGoods;
        }
    }
}
