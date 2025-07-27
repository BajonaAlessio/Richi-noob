namespace Backend.Utils;
namespace Backend.Models
{
    public class Pilot : IIdentifiable
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string Nazionalita { get; set; }
        public int Eta { get; set; }
        public int SquadraId { get; set; }
    }
}