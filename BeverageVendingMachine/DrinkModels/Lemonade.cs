using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeverageVendingMachine.DrinkModels
{
    public class Lemonade: AbstractDrink
    {
        bool IsSparkling { get; set; }

        public override bool Equals(object? obj)
        {
            return obj is Lemonade lemonade &&
                   Name == lemonade.Name &&
                   Price == lemonade.Price &&
                   IsSparkling == lemonade.IsSparkling;
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
    }
}
