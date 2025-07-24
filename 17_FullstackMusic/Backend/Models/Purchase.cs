using Backend.Utils;
using System.ComponentModel.DataAnnotations;

namespace Backend.Models
{
    public class Purchase : IIdentifiable
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "User è obbligatorio")]
        public int UserId { get; set; }

        [Required(ErrorMessage = "Album è obbligatorio")]
        public int AlbumId { get; set; }

        [Required(ErrorMessage = "quantità è obbligatoria")]
        [Range(1, 1000, ErrorMessage = "valore non valido")]
        public int Quantity { get; set; }
    }
}