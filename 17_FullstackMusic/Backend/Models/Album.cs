using Backend.Utils;
using System.ComponentModel.DataAnnotations;
namespace Backend.Models
{
    public class Album : IIdentifiable
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "il titolo è obbligatorio")]
        [StringLength(20, MinimumLength = 3, ErrorMessage = "il titolo non può superare i 20 caratteri")]
        public string Titolo { get; set; }

        [Required(ErrorMessage = "il nome della categoria è obbligatorio")]
        public int Anno { get; set; }

        [Required(ErrorMessage = "il titolo è obbligatorio")]
        [StringLength(20, MinimumLength = 3, ErrorMessage = "l'autore non può superare i 20 caratteri")]
        public string Autore { get; set; }

        [MinLength(1)]
        [MaxLength(30)]
        public List<int> CanzoniId { get; set; }

        [Required(ErrorMessage = "il genere è obbligatorio")]
        [StringLength(20, MinimumLength = 3, ErrorMessage = "l'autore non può superare i 20 caratteri")]
        public string Genere { get; set; }

        [Required(ErrorMessage = "Ascoltato obbligatorio")]
        public bool Ascoltato { get; set; }

    }
}