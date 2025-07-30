using Backend.Utils;
namespace Backend.Models
{
    public class PlayerDTO : IIdentifiable
    {
        public int Id { get; set; }

        public string PilotaNome { get; set; }

        public string MotoModello { get; set; }

        public string SquadraNome { get; set; }
    }
}