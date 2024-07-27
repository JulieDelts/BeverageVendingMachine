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

    }
}
