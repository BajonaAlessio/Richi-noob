namespace Backend.Utils;
namespace Backend.Models
{
    public class Gara : IIdentifiable
    {
        public int Id { get; set; }
        public DateTime Data { get; set; }
        public string Stato { get; set; }
        public string Clima { get; set; }
        public int CircuitoId { get; set; }
    }
}