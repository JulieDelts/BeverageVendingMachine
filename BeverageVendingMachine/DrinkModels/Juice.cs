using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeverageVendingMachine.DrinkModels
{
    public class Juice: Drink
    {
        public string Type { get; set; } = string.Empty;

        public int FruitAmountNeeded { get; set; } = 2;

        public override double Price { get; set; } = 300;
    }
}
