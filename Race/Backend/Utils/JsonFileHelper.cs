using System.Text.Json;

namespace Backend.Utils
{
    public static class JsonFileHelper
    {
        
        private static readonly JsonSerializerOptions options = new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true,
            WriteIndented = true
        };

        public static List<T> LoadList<T>(string filepath)
        {
            if (!Directory.Exists(filepath))
                throw new DirectoryNotFoundException($"Cartella {filepath} non trovata");

            List<string> filesNames = Directory.GetFiles(filepath).ToList();

            List<T> result = new List<T>();
            foreach (string fileName in filesNames)
            {
                string filesContent = File.ReadAllText(fileName);
                T entity = JsonSerializer.Deserialize<T>(filesContent);
                if (entity == null)
                    throw new JsonException($"contenuto di {fileName} non valido.");
                result.Add(entity);
            }
            return result;
        }

        public static void SaveList<T>(string folderPath, T entity)
        {
        if (!Directory.Exists(folderPath))
        Directory.CreateDirectory(folderPath); // <- Ahora crea la carpeta si no existe

        string fileContent = JsonSerializer.Serialize(entity, options);
        File.WriteAllText(WritePath<T>(folderPath, entity), fileContent);
        }

        public static string WritePath<T>(string folderPath, T entity)
        {
        var properties = typeof(T).GetProperties();
        string idFile = properties[0].GetValue(entity).ToString();
        string nomeFile = properties[1].GetValue(entity)?.ToString() ?? "Entity";

        // 🔥 Usa directamente la ruta que viene en Config.txt sin anteponer "Folders/"
        string pathJson = Path.Combine(folderPath, $"({idFile}){nomeFile}.json");

        return pathJson;
        }
    }
}