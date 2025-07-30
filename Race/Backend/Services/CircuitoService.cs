using Backend.Models;
using Backend.Utils;

namespace Backend.Services
{
    public class CircuitoService
    {
        private readonly List<Circuito> _circuiti = new();
        public string path;

        public CircuitoService()
        {
            ReadConfig();
            _circuiti = JsonFileHelper.LoadList<Circuito>(path);
        }

        public void Save(Circuito circuito)
        {
            JsonFileHelper.SaveList(path, circuito);
        }

        public void ReadConfig()
        {
            string[] lines = File.ReadAllLines(@"Txt_Files/Config.txt");
            path = lines[11];
        }

        public List<Circuito> GetAll()
        {
            List<Circuito> result = new List<Circuito>();
            foreach (var circuito in _circuiti)
            {
                result.Add(circuito);
            }
            return result;
        }

        public Circuito? GetById(int id)
        {
            foreach (var circuito in _circuiti)
            {
                if (circuito.Id == id)
                {
                    return circuito;
                }
            }
            return null;
        }

        public Circuito Add(Circuito newCircuito)
        {
            newCircuito.Id = IdGenerator.GenNextId<Circuito>(_circuiti);
            _circuiti.Add(newCircuito);
            LoggerHelper.Log($"Aggiunto circuito: {newCircuito.Id}({newCircuito.Nome})");
            Save(newCircuito);
            return newCircuito;
        }

        public bool Update(int id, Circuito updatedCircuito)
        {

            Circuito? existing = null;
            foreach (var a in _circuiti)
            {
                if (a.Id == id)
                {
                    existing = a;
                    break;
                }
            }
            if (existing == null)
            {
                LoggerHelper.Log($"Non riuscita l'azione aggiungere circuito: {updatedCircuito.Id}");
                return false;
            }

            string correctPath = JsonFileHelper.WritePath<Circuito>(path, existing);

            existing.Nome = updatedCircuito.Nome;
            existing.Localita = updatedCircuito.Localita;
            existing.Lunghezza = updatedCircuito.Lunghezza;
            existing.Giri = updatedCircuito.Giri;
            existing.Curve = updatedCircuito.Curve;
            
            File.Delete(correctPath);
            LoggerHelper.Log($"Aggiornato circuito: {existing.Id}({existing.Nome})"); 
            Save(existing);
            return true;
        }

        public bool Delete(int id)
        {
            Circuito circuito = GetById(id);
            if (circuito is null)
            {
                LoggerHelper.Log($"Non riuscita cancellazione circuito: {circuito.Id}"); 
                return false;
            }
            
            string correctPath = JsonFileHelper.WritePath<Circuito>(path, circuito);
            File.Delete(correctPath);
            LoggerHelper.Log($"Cancellato circuito: {circuito.Id}({circuito.Nome})");
            _circuiti.Remove(circuito);
            return true;
        }
        
    }  
}