using Backend.Utils;
namespace Backend.Models
{
    public class RaceDTO : IIdentifiable
    {
        public int Id { get; set; }
        public string PlayerNome { get; set; }
        public string SquadraNome { get; set; }
        public string MotoModello { get; set; }
        public string GaraNome { get; set; }
        public string CircuitoNome { get; set; }
        public double TempoFinale { get; set; }
        public int Posizione { get; set; }
    }
}