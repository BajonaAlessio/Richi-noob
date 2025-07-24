using System.Text.Json;
//using Newtonsoft.Json;

namespace Backend.Utils
{
    public static class JsonFileHelper
    {
        /// <summary>
        /// questa classe fornisce metodi per salvare e caricare liste di oggetti in file JSON
        /// questa è una classe statica che non può essere istanziata
        /// </summary>

        //configuro le impostazioni di serializzazione
        private static readonly JsonSerializerOptions options = new JsonSerializerOptions
        {
            //imposta le opzioni di serializzazione per gestire i nomi delle proprietà in modo case-insensitive
            PropertyNameCaseInsensitive = true,
            //imposta l'indentazione per una migliore leggibilità del file JSON
            WriteIndented = true
        };

        //metodo per caricare una lista di oggetti da un file JSON (deserializzazione)
        public static List<T> LoadList<T>(string filepath)
        {
            /*  VERSIONE LEZIONE
            //controlla se il file esiste, se non eiste restituisce una list vuota
            if (!filepath.Exist(filepath))
                return new List<T>();

            //legge il contenuto del file JSON
            string json = filepath.ReadAllText(filepath);
            return JsonSerializer.Deserialize<List<T>>(json, options) ?? new List<T>();
            */

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

        //metodo per salvare una lista di oggetti in un file JSON (serializzazione)
        public static void SaveList<T>(string filepath, T entity)
        {
            /* VERSIONE LEZIONE
            string json = JsonSerializer.Serialize(list, options);
            filepath.WriteAllText(filepath, json);*/

            if (!Directory.Exists(filepath))
                throw new DirectoryNotFoundException($"Cartella {filepath} non trovata");
        
            string fileContent = JsonSerializer.Serialize(entity, options);
            File.WriteAllText(WritePath<T>(filepath, entity), fileContent);

        }

        public static string WritePath<T>(string filePath,T entity)
        {
            var properties = typeof(T).GetProperties();
            string idFile = properties[0].GetValue(entity).ToString();
            string nomeFile = properties[1].GetValue(entity).ToString();
            string pathJson = $@"{filePath}/({idFile}){nomeFile}.json";
            string correctPath = pathJson.Replace(" ", "_");

            return correctPath;
        }
    }
}