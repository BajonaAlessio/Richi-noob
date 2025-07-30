using Backend.Models;
using Backend.Utils;

namespace Backend.Services
{
    public class GaraService
    {
        private readonly List<Gara> _gare = new();
        public string path;

        public GaraService()
        {
            ReadConfig();
            _gare = JsonFileHelper.LoadList<Gara>(path);
        }

        public void Save(Gara gara)
        {
            JsonFileHelper.SaveList(path, gara);
        }

        public void ReadConfig()
        {
            string[] lines = File.ReadAllLines(@"Txt_Files/Config.txt");
            path = lines[9];
        }

        public List<Gara> GetAll()
        {
            List<Gara> result = new List<Gara>();
            foreach (var gara in _gare)
            {
                result.Add(gara);
            }
            return result;
        }

        public Gara? GetById(int id)
        {
            foreach (var gara in _gare)
            {
                if (gara.Id == id)
                {
                    return gara;
                }
            }
            return null;
        }

        public Gara Add(Gara newGara)
        {
            newGara.Id = IdGenerator.GenNextId<Gara>(_gare);
            if (newGara.CircuitiId == null)
            newGara.CircuitiId = new List<int>();
            _gare.Add(newGara);
            LoggerHelper.Log($"Aggiunto gara: {newGara.Id}({newGara.Paese})");
            Save(newGara);
            return newGara;
        }

        public bool Update(int id, Gara updatedGara)
        {

            Gara? existing = null;
            foreach (var a in _gare)
            {
                if (a.Id == id)
                {
                    existing = a;
                    break;
                }
            }
            if (existing == null)
            {
                LoggerHelper.Log($"Non riuscita l'azione aggiungere gara: {updatedGara.Id}");
                return false;
            }

            string correctPath = JsonFileHelper.WritePath<Gara>(path, existing);

            existing.Paese = updatedGara.Paese;
            existing.Wheater = updatedGara.Wheater;
            existing.CircuitiId = updatedGara.CircuitiId;
            
            File.Delete(correctPath);
            LoggerHelper.Log($"Aggiornato gara: {existing.Id}({existing.Paese})"); 
            Save(existing);
            return true;
        }

        public bool Delete(int id)
        {
            Gara gara = GetById(id);
            if (gara is null)
            {
                LoggerHelper.Log($"Non riuscita cancellazione gara con Id: {id}"); 
                return false;
            }
            
            string correctPath = JsonFileHelper.WritePath<Gara>(path, gara);
            File.Delete(correctPath);
            LoggerHelper.Log($"Cancellato gara: {gara.Id}({gara.Paese})");
            _gare.Remove(gara);
            return true;
        }
        
    }  
}