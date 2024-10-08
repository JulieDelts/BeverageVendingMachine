﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace BeverageVendingMachine.DrinkModels
{
    public abstract class AbstractDrink
    {
        public virtual string Name { get; set; } = string.Empty;

        public virtual double Price { get; set; }

        public override string ToString()
        {
            return Name;
        }
    }
}
