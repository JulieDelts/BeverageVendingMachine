using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using BeverageVendingMachine.DrinkModels;

namespace BeverageVendingMachine.StorageUnits
{
    public class LemonadeTypesStorage: AbstractTypesStorage
    {
        public LemonadeTypesStorage(string path = "C:\\Users\\Юлия\\Desktop\\Программули\\BeverageVendingMachine\\LemonadeTypes.txt"): base(path) { }

        public  Dictionary<string, Lemonade> GetAllTypes()
        {
            string collection = string.Empty;

            using (StreamReader streamReader = new StreamReader(_filePath))
            {
                collection = streamReader.ReadToEnd();
            }

            Dictionary<string, Lemonade> lemonadeTypes;

            if (!string.IsNullOrEmpty(collection))
            {
                lemonadeTypes = JsonSerializer.Deserialize<Dictionary<string, Lemonade>>(collection) ?? new Dictionary<string, Lemonade>();
            }
            else
            {
                lemonadeTypes = new Dictionary<string, Lemonade>();
            }
            
            return lemonadeTypes;
        }

        public Lemonade GetType(string type)
        { 
            Dictionary<string, Lemonade> lemonadeTypes = GetAllTypes();

            if (lemonadeTypes.ContainsKey(type))
            {
                return lemonadeTypes[type];
            }
            else 
            { 
                return new Lemonade();
            }
        }

        public void Add(Lemonade type)
        {
            Dictionary<string, Lemonade> lemonadeTypes = GetAllTypes();

            lemonadeTypes.Add(type.Name, type);

            Save(lemonadeTypes);
        }

        public void Remove(string type)
        {
            Dictionary<string, Lemonade> lemonadeTypes = GetAllTypes();

            if (lemonadeTypes.ContainsKey(type))
            {
                lemonadeTypes.Remove(type);
            }

            Save(lemonadeTypes);
        }

        private void Save(Dictionary<string, Lemonade> types)
        {
            string json = JsonSerializer.Serialize<Dictionary<string, Lemonade>>(types);

            using (StreamWriter streamWriter = new StreamWriter(_filePath, false))
            {
                streamWriter.WriteLine(json);
            }
        }
    }
}
