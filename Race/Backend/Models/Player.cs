using Backend.Utils;
namespace Backend.Models
{
    public class Player : IIdentifiable
    {
        public int Id { get; set; }

        public int PilotaId { get; set; }

        public int MotoId { get; set; }

    }
}