using Backend.Models;
using Backend.Utils;

namespace Backend.Services
{
    public class MotoService
    {
        private readonly List<Moto> _motos = new();
        public string path;

        public MotoService()
        {
            ReadConfig();
            _motos = JsonFileHelper.LoadList<Moto>(path);
        }

        public void Save(Moto moto)
        {
            JsonFileHelper.SaveList(path, moto);
        }

        public void ReadConfig()
        {
            string[] lines = File.ReadAllLines(@"Txt_Files/Config.txt");
            path = lines[3];
        }

        public List<Moto> GetAll()
        {
            List<Moto> result = new List<Moto>();
            foreach (var moto in _motos)
            {
                result.Add(moto);
            }
            return result;
        }

        public Moto? GetById(int id)
        {
            foreach (var moto in _motos)
            {
                if (moto.Id == id)
                {
                    return moto;
                }
            }
            return null;
        }

        public Moto Add(Moto newMoto)
        {
            newMoto.Id = IdGenerator.GenNextId<Moto>(_motos);
            _motos.Add(newMoto);
            LoggerHelper.Log($"Aggiunto moto: {newMoto.Id}({newMoto.Marca})");
            Save(newMoto);
            return newMoto;
        }

        public bool Update(int id, Moto updatedMoto)
        {

            Moto? existing = null;
            foreach (var a in _motos)
            {
                if (a.Id == id)
                {
                    existing = a;
                    break;
                }
            }
            if (existing == null)
            {
                LoggerHelper.Log($"Non riuscita l'azione aggiungere moto: {updatedMoto.Id}");
                return false;
            }

            string correctPath = JsonFileHelper.WritePath<Moto>(path, existing);

            existing.Marca = updatedMoto.Marca;
            existing.Modello = updatedMoto.Modello;
            existing.Potenza = updatedMoto.Potenza;
            existing.Grip = updatedMoto.Grip ;
            existing.Inclinazione = updatedMoto.Inclinazione;
            existing.VelocitaMassima = updatedMoto.VelocitaMassima;
            existing.Accelerazione  = updatedMoto.Accelerazione;
            File.Delete(correctPath);
            LoggerHelper.Log($"Aggiornato moto: {existing.Id}({existing.Marca})"); 
            Save(existing);
            return true;
        }

        public bool Delete(int id)
        {
            Moto moto = GetById(id);
            if (moto is null)
            {
                LoggerHelper.Log($"Non riuscita cancellazione moto: {moto.Id}"); 
                return false;
            }
            
            string correctPath = JsonFileHelper.WritePath<Moto>(path, moto);
            File.Delete(correctPath);
            LoggerHelper.Log($"Cancellato moto: {moto.Id}({moto.Marca})");
            _motos.Remove(moto);
            return true;
        }
        
    }  
}