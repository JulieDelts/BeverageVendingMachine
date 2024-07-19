using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using BeverageVendingMachine.DrinkModels;

namespace BeverageVendingMachine.StorageUnits
{
    public class LemonadeTypesStorage
    {
        private string _filePath;

        public LemonadeTypesStorage(string path = "C:\\Users\\Юлия\\Desktop\\Программули\\BeverageVendingMachine\\LemonadeTypes.txt") 
        {
            _filePath = path;
        }

        public Dictionary<string, Lemonade> GetAllLemonadeTypes()
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

        public Lemonade GetLemonadeType(string lemonadeType)
        { 
            Dictionary<string, Lemonade> lemonadeTypes = GetAllLemonadeTypes();

            if (lemonadeTypes.ContainsKey(lemonadeType))
            {
                return lemonadeTypes[lemonadeType];
            }
            else 
            { 
                return new Lemonade();
            }
        }

        public void Add(Lemonade lemonadeType)
        {
            Dictionary<string, Lemonade> lemonadeTypes = GetAllLemonadeTypes();

            lemonadeTypes.Add(lemonadeType.Name, lemonadeType);

            Save(lemonadeTypes);
        }

        public void Remove(string lemonadeType)
        {
            Dictionary<string, Lemonade> lemonadeTypes = GetAllLemonadeTypes();

            if (lemonadeTypes.ContainsKey(lemonadeType))
            {
                lemonadeTypes.Remove(lemonadeType);
            }

            Save(lemonadeTypes);
        }

        private void Save(Dictionary<string, Lemonade> lemonadeTypes)
        {
            string json = JsonSerializer.Serialize<Dictionary<string, Lemonade>>(lemonadeTypes);

            using (StreamWriter streamWriter = new StreamWriter(_filePath, false))
            {
                streamWriter.WriteLine(json);
            }
        }
    }
}
