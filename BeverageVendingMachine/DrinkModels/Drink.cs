using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeverageVendingMachine.DrinkModels
{
    public abstract class Drink
    {
        public string Name { get; set; } = string.Empty;

        public virtual double Price { get; set; }
    }
}
