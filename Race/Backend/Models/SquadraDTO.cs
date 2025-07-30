using Backend.Utils;
namespace Backend.Models
{
    public class SquadraDTO : IIdentifiable
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string Paese { get; set; }
        public int AnnoFondazione { get; set; }
        public string Proprietario { get; set; }
        public List<Moto> Motos { get; set; }
    }
}