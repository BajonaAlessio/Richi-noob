using Backend.Models;
using Backend.Utils;

namespace Backend.Services
{
    public class AlbumService
    {
        private readonly List<Album> _albums = new();
        public string path;

        public AlbumService()
        {
            ReadConfig();
            _albums = JsonFileHelper.LoadList<Album>(path);
        }

        public void Save(Album album)
        {
            JsonFileHelper.SaveList(path, album);
        }

        public void ReadConfig()
        {
            string[] lines = File.ReadAllLines(@"Config.txt");
            //riga 1 di config: path salvataggio albums
            path = lines[0].Replace("path_Albums:", "");
        }

        public List<Album> GetAll()
        {
            List<Album> result = new List<Album>();
            foreach (var album in _albums)
            {
                result.Add(album);
            }
            return result;
        }

        public Album? GetById(int id)
        {
            foreach (var album in _albums)
            {
                if (album.Id == id)
                {
                    return album;
                }
            }
            return null;
        }

        public Album Add(Album newAlbum)
        {
            //ValidationHelper.AlbumValidation(newAlbum);
            newAlbum.Id = IdGenerator.GenNextId<Album>(_albums);
            _albums.Add(newAlbum);
            LoggerHelper.Log($"Aggiunto album: {newAlbum.Id}({newAlbum.Titolo})");
            Save(newAlbum);
            return newAlbum;
        }

        public bool Update(int id, Album updatedAlbum)
        {
            //ValidationHelper.AlbumValidation(updatedAlbum);

            Album? existing = null;
            foreach (var a in _albums) //sostituire con getbyid?
            {
                if (a.Id == id)
                {
                    existing = a;
                    break;
                }
            }
            if (existing == null)
            {
                LoggerHelper.Log($"Non riuscita l'azione aggiungere album: {updatedAlbum.Id}");
                return false;
            }

            string correctPath = JsonFileHelper.WritePath<Album>(path, existing);

            existing.Titolo = updatedAlbum.Titolo;
            existing.Anno = updatedAlbum.Anno;
            existing.Autore = updatedAlbum.Autore;
            existing.CanzoniId = updatedAlbum.CanzoniId;
            existing.Genere = updatedAlbum.Genere;
            existing.Ascoltato = updatedAlbum.Ascoltato;
            
            File.Delete(correctPath);
            LoggerHelper.Log($"Aggiornato album: {existing.Id}({existing.Titolo})"); 
            Save(existing);
            return true;
        }

        public bool Delete(int id)
        {
            Album album = GetById(id);
            if (album is null)
            {
                LoggerHelper.Log($"Non riuscita cancellazione album: {album.Id}"); 
                return false;
            }
            
            string correctPath = JsonFileHelper.WritePath<Album>(path, album);
            File.Delete(correctPath);
            LoggerHelper.Log($"Cancellato album: {album.Id}({album.Titolo})");
            _albums.Remove(album);
            return true;
        }
        
    }  
}