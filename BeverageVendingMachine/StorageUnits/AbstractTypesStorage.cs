using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BeverageVendingMachine.DrinkModels;

namespace BeverageVendingMachine.StorageUnits
{
    public abstract class AbstractTypesStorage
    {
        protected string _filePath;

        public AbstractTypesStorage(string path)
        {
            _filePath = path;
        }
    }
}
