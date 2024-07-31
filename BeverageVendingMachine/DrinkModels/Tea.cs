using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeverageVendingMachine.DrinkModels
{
    public class Tea: AbstractDrink
    {
        public double Sugar { get; set; }

        public double Water { get; set; }

        public override bool Equals(object? obj)
        {
            return obj is Tea tea &&
                   Name == tea.Name &&
                   Price == tea.Price &&
                   Sugar == tea.Sugar &&
                   Water == tea.Water;
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
    }
}
