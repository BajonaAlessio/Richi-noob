using Backend.Utils;
namespace Backend.Models
{
    public class AlbumDTO
    {
        public int Id { get; set; }
        public string Titolo { get; set; }
        public int Anno { get; set; }
        public string Autore { get; set; }
        public List<Canzone> Canzoni { get; set; }
        public string Genere { get; set; }
        public bool Ascoltato { get; set; }

    }
}