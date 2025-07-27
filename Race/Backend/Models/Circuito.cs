namespace Backend.Models
{
    public class Circuito : IIdentifiable
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string Localita { get; set; }
        public int Lunghezza { get; set; }
    }
}