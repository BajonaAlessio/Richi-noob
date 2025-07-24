using Backend.Models;
using Backend.Utils;

namespace Backend.Services
{
    public class CanzoneService
    {
        private readonly List<Canzone> _canzones = new();
        public string path;

        public CanzoneService()
        {
            ReadConfig();
            _canzones = JsonFileHelper.LoadList<Canzone>(path);
        }

        public void Save(Canzone canzone)
        {
            JsonFileHelper.SaveList(path, canzone);
        }

        public void ReadConfig()
        {
            string[] lines = File.ReadAllLines(@"Config.txt");
            //riga 1 di config: path salvataggio canzones
            path = lines[3].Replace("path_Canzoni:", "");
        }

        public List<Canzone> GetAll()
        {
            List<Canzone> result = new List<Canzone>();
            foreach (var canzone in _canzones)
            {
                result.Add(canzone);
            }
            return result;
        }

        public Canzone? GetById(int id)
        {
            foreach (var canzone in _canzones)
            {
                if (canzone.Id == id)
                {
                    return canzone;
                }
            }
            return null;
        }

        public Canzone Add(Canzone newCanzone)
        {
            //ValidationHelper.CanzoneValidation(newCanzone);
            newCanzone.Id = IdGenerator.GenNextId<Canzone>(_canzones);
            _canzones.Add(newCanzone);
            LoggerHelper.Log($"Aggiunto canzone: {newCanzone.Id}({newCanzone.Nome})");
            Save(newCanzone);
            return newCanzone;
        }

        public bool Update(int id, Canzone updatedCanzone)
        {
            //ValidationHelper.CanzoneValidation(updatedCanzone);

            Canzone? existing = null;
            foreach (var a in _canzones) //sostituire con getbyid?
            {
                if (a.Id == id)
                {
                    existing = a;
                    break;
                }
            }
            if (existing == null)
            {
                LoggerHelper.Log($"Non riuscita l'azione aggiungere canzone: {updatedCanzone.Id}");
                return false;
            }

            string correctPath = JsonFileHelper.WritePath<Canzone>(path, existing);

            existing.Nome = updatedCanzone.Nome;
            existing.Durata = updatedCanzone.Durata;
            
            File.Delete(correctPath);
            LoggerHelper.Log($"Aggiornato canzone: {existing.Id}({existing.Nome})"); 
            Save(existing);
            return true;
        }

        public bool Delete(int id)
        {
            Canzone canzone = GetById(id);
            if (canzone is null)
            {
                LoggerHelper.Log($"Non riuscita cancellazione canzone: {canzone.Id}"); 
                return false;
            }
            
            string correctPath = JsonFileHelper.WritePath<Canzone>(path, canzone);
            File.Delete(correctPath);
            LoggerHelper.Log($"Cancellato canzone: {canzone.Id}({canzone.Nome})");
            _canzones.Remove(canzone);
            return true;
        }
        
    }  
}