using Backend.Models;
using Backend.Utils;

namespace Backend.Services
{
    public class SquadraService
    {
        private readonly List<Squadra> _squadre = new();
        public string path;

        public SquadraService()
        {
            ReadConfig();
            _squadre = JsonFileHelper.LoadList<Squadra>(path);
        }

        public void Save(Squadra squadra)
        {
            JsonFileHelper.SaveList(path, squadra);
        }

        public void ReadConfig()
        {
            string[] lines = File.ReadAllLines(@"Txt_Files/Config.txt");
            path = lines[5];
        }

        public List<Squadra> GetAll()
        {
            List<Squadra> result = new List<Squadra>();
            foreach (var squadra in _squadre)
            {
                result.Add(squadra);
            }
            return result;
        }

        public Squadra? GetById(int id)
        {
            foreach (var squadra in _squadre)
            {
                if (squadra.Id == id)
                {
                    return squadra;
                }
            }
            return null;
        }

        public Squadra Add(Squadra newSquadra)
        {
            newSquadra.Id = IdGenerator.GenNextId<Squadra>(_squadre);
            _squadre.Add(newSquadra);
            LoggerHelper.Log($"Aggiunto squadra: {newSquadra.Id}({newSquadra.Nome})");
            Save(newSquadra);
            return newSquadra;
        }

        public bool Update(int id, Squadra updatedSquadra)
        {

            Squadra? existing = null;
            foreach (var a in _squadre)
            {
                if (a.Id == id)
                {
                    existing = a;
                    break;
                }
            }
            if (existing == null)
            {
                LoggerHelper.Log($"Non riuscita l'azione aggiungere squadra: {updatedSquadra.Id}");
                return false;
            }

            string correctPath = JsonFileHelper.WritePath<Squadra>(path, existing);

            existing.Nome = updatedSquadra.Nome;
            existing.Paese = updatedSquadra.Paese;
            existing.AnnoFondazione = updatedSquadra.AnnoFondazione;
            existing.Proprietario = updatedSquadra.Proprietario;
            existing.MotosId = updatedSquadra.MotosId;
            
            File.Delete(correctPath);
            LoggerHelper.Log($"Aggiornato squadra: {existing.Id}({existing.Nome})"); 
            Save(existing);
            return true;
        }

        public bool Delete(int id)
        {
            Squadra squadra = GetById(id);
            if (squadra is null)
            {
                LoggerHelper.Log($"Non riuscita cancellazione squadra con Id: {id}"); 
                return false;
            }
            
            string correctPath = JsonFileHelper.WritePath<Squadra>(path, squadra);
            File.Delete(correctPath);
            LoggerHelper.Log($"Cancellato squadra: {squadra.Id}({squadra.Nome})");
            _squadre.Remove(squadra);
            return true;
        }
        
    }  
}