using BeverageVendingMachine.DrinkModels;
using System.Text.Json;

namespace BeverageVendingMachine.StorageUnits
{
    public class CoffeeTypesStorage
    {
        private string _filePath;

        public CoffeeTypesStorage(string path = "C:\\Users\\Юлия\\Desktop\\Программули\\BeverageVendingMachine\\CoffeeTypes.txt")
        {
            _filePath = path;
        }

        public Dictionary<string, Coffee> GetAllCoffeeTypes()
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

        public Coffee GetCoffeeType(string coffeeType)
        {
            Dictionary<string, Coffee> coffeeTypes = GetAllCoffeeTypes();

            if (coffeeTypes.ContainsKey(coffeeType))
            {
                return coffeeTypes[coffeeType];
            }
            else
            {
                return new Coffee();
            }
        }

        public void Add(Coffee coffeeType)
        {
            Dictionary<string, Coffee> coffeeTypes = GetAllCoffeeTypes();

            coffeeTypes.Add(coffeeType.Name, coffeeType);

            Save(coffeeTypes);
        }

        public void Remove(string coffeeType)
        {
            Dictionary<string, Coffee> coffeeTypes = GetAllCoffeeTypes();

            if (coffeeTypes.ContainsKey(coffeeType))
            {
                coffeeTypes.Remove(coffeeType);
            }

            Save(coffeeTypes);
        }

        private void Save(Dictionary<string, Coffee> coffeeTypes)
        {
            string json = JsonSerializer.Serialize<Dictionary<string, Coffee>>(coffeeTypes);

            using (StreamWriter streamWriter = new StreamWriter(_filePath, false))
            {
                streamWriter.WriteLine(json);
            }
        }
    }
}
