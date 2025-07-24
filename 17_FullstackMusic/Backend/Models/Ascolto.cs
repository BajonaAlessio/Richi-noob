using Backend.Utils;
namespace Backend.Models
{
    public class Ascolto : IIdentifiable
    {
        public int Id { get; set; }
        public int UtenteId { get; set; }
        public int CanzoneId { get; set; }
    }
}