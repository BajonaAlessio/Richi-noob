using Backend.Utils;
namespace Backend.Models
{
    public class Squadra : IIdentifiable
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string Paese { get; set; }
        public int AnnoFondazione { get; set; }
        public string Proprietario { get; set; }
        public List<int> MotosId { get; set; }
    }
}