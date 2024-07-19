using System.Text.Json;
using BeverageVendingMachine.DrinkModels;

namespace BeverageVendingMachine.StorageUnits
{
    public class TeaTypesStorage: AbstractTypesStorage
    {
        public TeaTypesStorage(string path = "C:\\Users\\Юлия\\Desktop\\Программули\\BeverageVendingMachine\\TeaTypes.txt"): base(path) { }

        public Dictionary<string, Tea> GetAllTypes()
        {
            string collection = string.Empty;

            using (StreamReader streamReader = new StreamReader(_filePath))
            {
                collection = streamReader.ReadToEnd();
            }

            Dictionary<string, Tea> teaTypes;

            if (!string.IsNullOrEmpty(collection))
            {
                teaTypes = JsonSerializer.Deserialize<Dictionary<string, Tea>>(collection) ?? new Dictionary<string, Tea>();
            }
            else
            {
                teaTypes = new Dictionary<string, Tea>();
            }

            return teaTypes;
        }

        public Tea GetType(string type)
        {
            Dictionary<string, Tea> teaTypes = GetAllTypes();

            if (teaTypes.ContainsKey(type))
            {
                return teaTypes[type];
            }
            else
            {
                return new Tea();
            }
        }

        public void Add(Tea type)
        {
            Dictionary<string, Tea> teaTypes = GetAllTypes();

            teaTypes.Add(type.Name, type);

            Save(teaTypes);
        }

        public void Remove(string type)
        {
            Dictionary<string, Tea> teaTypes = GetAllTypes();

            if (teaTypes.ContainsKey(type))
            {
                teaTypes.Remove(type);
            }

            Save(teaTypes);
        }

        private void Save(Dictionary<string, Tea> types)
        {
            string json = JsonSerializer.Serialize<Dictionary<string, Tea>>(types);

            using (StreamWriter streamWriter = new StreamWriter(_filePath, false))
            {
                streamWriter.WriteLine(json);
            }
        }
    }
}
