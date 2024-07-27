using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeverageVendingMachine.DrinkModels
{
    public class HotChocolate : Drink
    {

        public double CocoaPowder { get; set; } = 30;

        public double MilkPowder { get; set; } = 30;

        public double Water { get; set; } = 400;

        public double Sugar { get; set; } = 20;

        public override string Name { get; set; } = "hot chocolate";

        public override double Price { get; set; } = 250;
    }
}
