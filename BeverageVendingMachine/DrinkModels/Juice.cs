using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeverageVendingMachine.DrinkModels
{
    public class Juice: AbstractDrink
    {
        public string Type { get; set; } = string.Empty;

        public int FruitAmountNeeded { get; set; } = 2;

        public override double Price { get; set; } = 300;

        public override bool Equals(object? obj)
        {
            return obj is Juice juice &&
                   Name == juice.Name &&
                   Price == juice.Price &&
                   Type == juice.Type &&
                   FruitAmountNeeded == juice.FruitAmountNeeded &&
                   Price == juice.Price;
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
    }
}
