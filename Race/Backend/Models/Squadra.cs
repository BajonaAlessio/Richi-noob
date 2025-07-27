namespace Backend.Utils;
namespace Backend.Models
{
    public class Squadra : IIdentifiable
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string Paese { get; set; }
    }
}