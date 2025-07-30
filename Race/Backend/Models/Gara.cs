using Backend.Utils;
namespace Backend.Models
{
    public class Gara : IIdentifiable
    {
        public int Id { get; set; }
        public string Paese { get; set; }
        public string Wheater { get; set; }
        public List<int> CircuitiId { get; set; }
    }
}