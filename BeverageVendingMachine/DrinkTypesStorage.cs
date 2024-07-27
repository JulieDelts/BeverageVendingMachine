using System.Reflection.Metadata;
using System.Text.Json;
using BeverageVendingMachine.DrinkModels;

namespace BeverageVendingMachine
{
    public class DrinkTypesStorage<T> where T: Drink
    {
        private readonly string _storagePath;

        public DrinkTypesStorage(string path)
        {
            _storagePath = path;
        }

        public Dictionary<string, T> GetAllTypes()
        {
            if (!string.IsNullOrEmpty(_storagePath))
            {
                string collection = string.Empty;

                using (StreamReader streamReader = new StreamReader(_storagePath))
                {
                    collection = streamReader.ReadToEnd();
                }

                Dictionary<string, T> drinkTypes;

                if (!string.IsNullOrEmpty(collection))
                {
                    drinkTypes = JsonSerializer.Deserialize<Dictionary<string, T>>(collection) ?? new Dictionary<string, T>();
                }
                else
                {
                    drinkTypes = new Dictionary<string, T>();
                }

                return drinkTypes;
            }
            else
            {
                throw new ArgumentException("The file path is provided incorrectly.");
            }
        }

        public T GetType(string type)
        {
            Dictionary<string, T> drinkTypes = GetAllTypes();

            if (drinkTypes.ContainsKey(type))
            {
                return drinkTypes[type];
            }
            else
            {
                throw new ArgumentException("The drink type is not present in the storage.");
            }
        }

        public void Add(T drinkType)
        {
            Dictionary<string, T> drinkTypes = GetAllTypes();

            drinkTypes.Add(drinkType.Name, drinkType);

            Save(drinkTypes);
        }

        public void Remove(string drinkType)
        {
            Dictionary<string, T> drinkTypes = GetAllTypes();

            if (drinkTypes.ContainsKey(drinkType))
            {
                drinkTypes.Remove(drinkType);
                Save(drinkTypes);
            }
        }

        public override string ToString()
        {
            return $"Storage: {_storagePath}";
        }

        public override bool Equals(object? obj)
        {
            return obj is DrinkTypesStorage<T> storage &&
                   _storagePath == storage._storagePath;
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        private void Save(Dictionary<string, T> types)
        {
            string json = JsonSerializer.Serialize(types);

            using (StreamWriter streamWriter = new StreamWriter(_storagePath, false))
            {
                streamWriter.WriteLine(json);
            }
        }
    }
}
