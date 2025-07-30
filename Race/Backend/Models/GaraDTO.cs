using Backend.Utils;
namespace Backend.Models
{
    public class GaraDTO : IIdentifiable
    {
        public int Id { get; set; }
        public string Paese { get; set; }
        public string Wheater { get; set; }
        public List<Circuito> Circuiti { get; set; }

    }
}