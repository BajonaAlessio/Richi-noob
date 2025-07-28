using Backend.Models;
using Backend.Utils;

namespace Backend.Services
{
    public class AscoltoService
    {
        private readonly List<Ascolto> _ascoltos = new();
        public string path;

        public AscoltoService()
        {
            ReadConfig();
            _ascoltos = JsonFileHelper.LoadList<Ascolto>(path);
        }

        public void Save(Ascolto ascolto)
        {
            JsonFileHelper.SaveList(path, ascolto);
        }

        public void ReadConfig()
        {
            string[] lines = File.ReadAllLines(@"Config.txt");
            //riga 1 di config: path salvataggio ascoltos
            path = lines[0].Replace("path_Ascoltos:", "");
        }

        public List<Ascolto> GetAll()
        {
            List<Ascolto> result = new List<Ascolto>();
            foreach (var ascolto in _ascoltos)
            {
                result.Add(ascolto);
            }
            return result;
        }

        public Ascolto? GetById(int id)
        {
            foreach (var ascolto in _ascoltos)
            {
                if (ascolto.Id == id)
                {
                    return ascolto;
                }
            }
            return null;
        }

        public Ascolto Add(Ascolto newAscolto)
        {
            newAscolto.Id = IdGenerator.GenNextId<Ascolto>(_ascoltos);
            _ascoltos.Add(newAscolto);
            LoggerHelper.Log($"Aggiunto ascolto: {newAscolto.Id}");
            Save(newAscolto);
            return newAscolto;
        }

        public bool Update(int id, Ascolto updatedAscolto)
        {

            Ascolto? existing = null;
            foreach (var a in _ascoltos) //sostituire con getbyid?
            {
                if (a.Id == id)
                {
                    existing = a;
                    break;
                }
            }
            if (existing == null)
            {
                LoggerHelper.Log($"Non riuscita l'azione aggiungere ascolto: {updatedAscolto.Id}");
                return false;
            }

            string correctPath = JsonFileHelper.WritePath<Ascolto>(path, existing);

            existing.UtenteId  = updatedAscolto.UtenteId ;
            existing.CanzoneId = updatedAscolto.CanzoneId;
            
            File.Delete(correctPath);
            LoggerHelper.Log($"Aggiornato ascolto: {existing.Id}"); 
            Save(existing);
            return true;
        }

        public bool Delete(int id)
        {
            Ascolto ascolto = GetById(id);
            if (ascolto is null)
            {
                LoggerHelper.Log($"Non riuscita cancellazione ascolto: {ascolto.Id}"); 
                return false;
            }
            
            string correctPath = JsonFileHelper.WritePath<Ascolto>(path, ascolto);
            File.Delete(correctPath);
            LoggerHelper.Log($"Cancellato ascolto: {ascolto.Id}");
            _ascoltos.Remove(ascolto);
            return true;
        }
        
    }  
}