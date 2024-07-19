using BeverageVendingMachine.DrinkModels;
using System.Text.Json;

namespace BeverageVendingMachine.StorageUnits
{
    public class CoffeeTypesStorage: AbstractTypesStorage
    {
        public CoffeeTypesStorage(string path = "C:\\Users\\Юлия\\Desktop\\Программули\\BeverageVendingMachine\\CoffeeTypes.txt"): base(path) { }
 
        public Dictionary<string, Coffee> GetAllTypes()
        {
            string collection = string.Empty;

            using (StreamReader streamReader = new StreamReader(_filePath))
            {
                collection = streamReader.ReadToEnd();
            }

            Dictionary<string, Coffee> CoffeeTypes;

            if (!string.IsNullOrEmpty(collection))
            {
                CoffeeTypes = JsonSerializer.Deserialize<Dictionary<string, Coffee>>(collection) ?? new Dictionary<string, Coffee>();
            }
            else
            {
                CoffeeTypes = new Dictionary<string, Coffee>();
            }

            return CoffeeTypes;
        }

        public Coffee GetType(string type)
        {
            Dictionary<string, Coffee> coffeeTypes = GetAllTypes();

            if (coffeeTypes.ContainsKey(type))
            {
                return coffeeTypes[type];
            }
            else
            {
                return new Coffee();
            }
        }

        public void Add(Coffee type)
        {
            Dictionary<string, Coffee> coffeeTypes = GetAllTypes();

            coffeeTypes.Add(type.Name, type);

            Save(coffeeTypes);
        }

        public void Remove(string type)
        {
            Dictionary<string, Coffee> coffeeTypes = GetAllTypes();

            if (coffeeTypes.ContainsKey(type))
            {
                coffeeTypes.Remove(type);
            }

            Save(coffeeTypes);
        }

        private void Save(Dictionary<string, Coffee> types)
        {
            string json = JsonSerializer.Serialize<Dictionary<string, Coffee>>(types);

            using (StreamWriter streamWriter = new StreamWriter(_filePath, false))
            {
                streamWriter.WriteLine(json);
            }
        }
    }
}
