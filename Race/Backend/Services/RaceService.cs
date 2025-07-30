using Backend.Models;
using Backend.Utils;

namespace Backend.Services
{
    public class RaceService
    {
        private readonly List<Race> _races = new();
        public string path;

        public RaceService()
        {
            ReadConfig();
            _races = JsonFileHelper.LoadList<Race>(path);
        }

        public void Save(Race race)
        {
            JsonFileHelper.SaveList(path, race);
        }

        public void ReadConfig()
        {
            string[] lines = File.ReadAllLines(@"Txt_Files/Config.txt");
            path = lines[13];
        }

        public List<Race> GetAll()
        {
            List<Race> result = new List<Race>();
            foreach (var race in _races)
            {
                result.Add(race);
            }
            return result;
        }

        public Race? GetById(int id)
        {
            foreach (var race in _races)
            {
                if (race.Id == id)
                {
                    return race;
                }
            }
            return null;
        }

        public Race Add(Race newRace)
        {
            newRace.Id = IdGenerator.GenNextId<Race>(_races);
            _races.Add(newRace);
            LoggerHelper.Log($"Aggiunto race: {newRace.Id}");
            Save(newRace);
            return newRace;
        }

        public bool Update(int id, Race updatedRace)
        {

            Race? existing = null;
            foreach (var a in _races)
            {
                if (a.Id == id)
                {
                    existing = a;
                    break;
                }
            }
            if (existing == null)
            {
                LoggerHelper.Log($"Non riuscita l'azione aggiungere race: {updatedRace.Id}");
                return false;
            }

            string correctPath = JsonFileHelper.WritePath<Race>(path, existing);

            existing.PlayerId = updatedRace.PlayerId;
            existing.GaraId = updatedRace.GaraId;
            existing.TempoFinale = updatedRace.TempoFinale;
            existing.Posizione = updatedRace.Posizione;
            
            File.Delete(correctPath);
            LoggerHelper.Log($"Aggiornato race: {existing.Id}"); 
            Save(existing);
            return true;
        }

        public bool Delete(int id)
        {
            Race race = GetById(id);
            if (race is null)
            {
                LoggerHelper.Log($"Non riuscita cancellazione race: {race.Id}"); 
                return false;
            }
            
            string correctPath = JsonFileHelper.WritePath<Race>(path, race);
            File.Delete(correctPath);
            LoggerHelper.Log($"Cancellato race: {race.Id}");
            _races.Remove(race);
            return true;
        }
        
    }  
}