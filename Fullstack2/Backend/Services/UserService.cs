using Backend.Models;
using Backend.Utils;

namespace Backend.Services
{
    public class UserService
    {
        private readonly List<User> _users = new();

        public string path;

        public UserService()
        {
            ReadConfig();
            _users = JsonFileHelper.LoadList<User>(path);
        }

        public void Save(User user)
        {
            JsonFileHelper.SaveList(path, user);
        }

        public void ReadConfig()
        {
            string[] lines = File.ReadAllLines(@"Config.txt");
            //riga 1 di config: path salvataggio albums
            path = lines[1].Replace("path_User:", "");
        }

        public List<User> GetAll()
        {
            List<User> result = new List<User>();
            foreach (var user in _users)
            {
                result.Add(user);
            }
            return result;
        }

        public User? GetById(int id)
        {
            foreach (var user in _users)
            {
                if (user.Id == id)
                {
                    return user;
                }
            }
            return null;
        }

        public User Add(User newUser)
        {
            //ValidationHelper.UserValidation(newUser);
            newUser.Id = IdGenerator.GenNextId<User>(_users);
            _users.Add(newUser);
            LoggerHelper.Log($"Aggiunto user: {newUser.Id}({newUser.NomeUtente})");
            Save(newUser);
            return newUser;
        }

        public bool Update(int id, User updatedUser)
        {
            //ValidationHelper.UserValidation(updatedUser);
            User? existing = null;
            foreach (var a in _users) //sostituire con getbyid?
            {
                if (a.Id == id)
                {
                    existing = a;
                    break;
                }
            }
            if (existing == null)
            {
                LoggerHelper.Log($"Non riuscita l'azione aggiungere user: {updatedUser.Id}");
                return false;
            }

            string correctPath = JsonFileHelper.WritePath<User>(path, existing);

            existing.NomeUtente = updatedUser.NomeUtente;
            existing.Indirizzo = updatedUser.Indirizzo;

            
            File.Delete(correctPath);
            LoggerHelper.Log($"Aggiornato User: {existing.Id}({existing.NomeUtente})");
            Save(existing);

            return true;
        }

        public bool Delete(int id)
        {
            User user = GetById(id);
            if (user is null)
            {
                LoggerHelper.Log($"Non riuscita rimozione user: {user.Id}({user.NomeUtente})");  
                return false;
            }    
            string correctPath = JsonFileHelper.WritePath<User>(path, user);
            File.Delete(correctPath);
            LoggerHelper.Log($"Rimosso user: {user.Id}({user.NomeUtente})");  
            _users.Remove(user);
            return true;
        }
    }
}