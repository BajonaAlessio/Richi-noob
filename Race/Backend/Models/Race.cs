using Backend.Utils;
namespace Backend.Models
{
    public class Race : IIdentifiable
    {
        public int Id { get; set; }
        public int PlayerId { get; set; }
        public int GaraId { get; set; }
        public double TempoFinale { get; set; }
        public int Posizione { get; set; }

    }
}