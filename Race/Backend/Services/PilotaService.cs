using Backend.Models;
using Backend.Utils;

namespace Backend.Services
{
    public class PilotaService
    {
        private readonly List<Pilota> _piloti = new();
        public string path;

        public PilotaService()
        {
            ReadConfig();
            _piloti = JsonFileHelper.LoadList<Pilota>(path);
        }

        public void Save(Pilota pilota)
        {
            JsonFileHelper.SaveList(path, pilota);
        }

        public void ReadConfig()
        {
            string[] lines = File.ReadAllLines(@"Txt_Files/Config.txt");
            path = lines[1];
        }

        public List<Pilota> GetAll()
        {
            List<Pilota> result = new List<Pilota>();
            foreach (var pilota in _piloti)
            {
                result.Add(pilota);
            }
            return result;
        }

        public Pilota? GetById(int id)
        {
            foreach (var pilota in _piloti)
            {
                if (pilota.Id == id)
                {
                    return pilota;
                }
            }
            return null;
        }

        public Pilota Add(Pilota newPilota)
        {
            newPilota.Id = IdGenerator.GenNextId<Pilota>(_piloti);
            _piloti.Add(newPilota);
            LoggerHelper.Log($"Aggiunto pilota: {newPilota.Id}({newPilota.Nome})");
            Save(newPilota);
            return newPilota;
        }

        public bool Update(int id, Pilota updatedPilota)
        {

            Pilota? existing = null;
            foreach (var a in _piloti)
            {
                if (a.Id == id)
                {
                    existing = a;
                    break;
                }
            }
            if (existing == null)
            {
                LoggerHelper.Log($"Non riuscita l'azione aggiungere pilota: {updatedPilota.Id}");
                return false;
            }

            string correctPath = JsonFileHelper.WritePath<Pilota>(path, existing);

            existing.Nome = updatedPilota.Nome;
            existing.Nazionalita = updatedPilota.Nazionalita;
            existing.Eta = updatedPilota.Eta;
            existing.Altezza = updatedPilota.Altezza;
            existing.Peso = updatedPilota.Peso;
            existing.SquadraId = updatedPilota.SquadraId;

            
            File.Delete(correctPath);
            LoggerHelper.Log($"Aggiornato pilota: {existing.Id}({existing.Nome})"); 
            Save(existing);
            return true;
        }

        public bool Delete(int id)
        {
            Pilota pilota = GetById(id);
            if (pilota is null)
            {
                LoggerHelper.Log($"Non riuscita cancellazione pilota: {pilota.Id}"); 
                return false;
            }
            
            string correctPath = JsonFileHelper.WritePath<Pilota>(path, pilota);
            File.Delete(correctPath);
            LoggerHelper.Log($"Cancellato pilota: {pilota.Id}({pilota.Nome})");
            _piloti.Remove(pilota);
            return true;
        }
        
    }  
}