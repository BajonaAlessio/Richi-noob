using Backend.Utils;
namespace Backend.Models
{
    public class PilotaDTO : IIdentifiable
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string Nazionalita { get; set; }
        public int Eta { get; set; }
        public double Altezza { get; set; }
        public double Peso { get; set; }
        public string SquadraNome { get; set; }
    }
}