using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeverageVendingMachine.DrinkModels
{
    public class Coffee: AbstractDrink
    {
        public double Sugar { get; set; }

        public double Water { get; set; }

        public double CoffeePowder { get; set; }

        public double MilkPowder { get; set; }

        public override bool Equals(object? obj)
        {
            return obj is Coffee coffee &&
                   Name == coffee.Name &&
                   Price == coffee.Price &&
                   Sugar == coffee.Sugar &&
                   Water == coffee.Water &&
                   CoffeePowder == coffee.CoffeePowder &&
                   MilkPowder == coffee.MilkPowder;
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
    }
}
