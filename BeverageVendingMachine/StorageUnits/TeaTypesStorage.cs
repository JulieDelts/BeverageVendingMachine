using System.Text.Json;
using BeverageVendingMachine.DrinkModels;

namespace BeverageVendingMachine.StorageUnits
{
    public class TeaTypesStorage
    {
        private string _filePath;

        public TeaTypesStorage(string path = "C:\\Users\\Юлия\\Desktop\\Программули\\BeverageVendingMachine\\TeaTypes.txt")
        {
            _filePath = path;
        }

        public Dictionary<string, Tea> GetAllTeaTypes()
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

        public Tea GetTeaType(string teaType)
        {
            Dictionary<string, Tea> teaTypes = GetAllTeaTypes();

            if (teaTypes.ContainsKey(teaType))
            {
                return teaTypes[teaType];
            }
            else
            {
                return new Tea();
            }
        }

        public void Add(Tea teaType)
        {
            Dictionary<string, Tea> teaTypes = GetAllTeaTypes();

            teaTypes.Add(teaType.Name, teaType);

            Save(teaTypes);
        }

        public void Remove(string teaType)
        {
            Dictionary<string, Tea> teaTypes = GetAllTeaTypes();

            if (teaTypes.ContainsKey(teaType))
            {
                teaTypes.Remove(teaType);
            }

            Save(teaTypes);
        }

        private void Save(Dictionary<string, Tea> teaTypes)
        {
            string json = JsonSerializer.Serialize<Dictionary<string, Tea>>(teaTypes);

            using (StreamWriter streamWriter = new StreamWriter(_filePath, false))
            {
                streamWriter.WriteLine(json);
            }
        }
    }
}
