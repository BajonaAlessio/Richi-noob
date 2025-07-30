using Backend.Models;
using Backend.Utils;

namespace Backend.Services
{
    public class PlayerService
    {
        private readonly List<Player> _players = new();
        public string path;

        public PlayerService()
        {
            ReadConfig();
            _players = JsonFileHelper.LoadList<Player>(path);
        }

        public void Save(Player player)
        {
            JsonFileHelper.SaveList(path, player);
        }

        public void ReadConfig()
        {
            string[] lines = File.ReadAllLines(@"Txt_Files/Config.txt");
            path = lines[7];
        }

        public List<Player> GetAll()
        {
            List<Player> result = new List<Player>();
            foreach (var player in _players)
            {
                result.Add(player);
            }
            return result;
        }

        public Player? GetById(int id)
        {
            foreach (var player in _players)
            {
                if (player.Id == id)
                {
                    return player;
                }
            }
            return null;
        }

        public Player Add(Player newPlayer)
        {
            newPlayer.Id = IdGenerator.GenNextId<Player>(_players);
            _players.Add(newPlayer);
            LoggerHelper.Log($"Aggiunto player: {newPlayer.Id}");
            Save(newPlayer);
            return newPlayer;
        }

        public bool Update(int id, Player updatedPlayer)
        {

            Player? existing = null;
            foreach (var a in _players)
            {
                if (a.Id == id)
                {
                    existing = a;
                    break;
                }
            }
            if (existing == null)
            {
                LoggerHelper.Log($"Non riuscita l'azione aggiungere player: {updatedPlayer.Id}");
                return false;
            }

            string correctPath = JsonFileHelper.WritePath<Player>(path, existing);

            existing.PilotaId = updatedPlayer.PilotaId;
            existing.MotoId = updatedPlayer.MotoId;

            
            File.Delete(correctPath);
            LoggerHelper.Log($"Aggiornato player: {existing.Id}"); 
            Save(existing);
            return true;
        }

        public bool Delete(int id)
        {
            Player player = GetById(id);
            if (player is null)
            {
                LoggerHelper.Log($"Non riuscita cancellazione player: {player.Id}"); 
                return false;
            }
            
            string correctPath = JsonFileHelper.WritePath<Player>(path, player);
            File.Delete(correctPath);
            LoggerHelper.Log($"Cancellato player: {player.Id}");
            _players.Remove(player);
            return true;
        }
        
    }  
}