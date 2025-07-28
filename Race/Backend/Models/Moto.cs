namespace Backend.Utils;
namespace Backend.Models
{
    public class Moto : IIdentifiable
    {
        public int Id { get; set; }
        public string Marca { get; set; }
        public string Modello { get; set; }
        public int SquadraId { get; set; }
    }
}