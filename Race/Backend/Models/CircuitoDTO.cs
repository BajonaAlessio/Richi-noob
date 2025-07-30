using Backend.Utils;
namespace Backend.Models
{
    public class CircuitoDTO : IIdentifiable
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string Localita { get; set; }
        public double Lunghezza { get; set; }
        public int Giri { get; set; }
        public int Curve { get; set; }

    }
}