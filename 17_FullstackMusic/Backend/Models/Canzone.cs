using Backend.Utils;
using System.ComponentModel.DataAnnotations;

namespace Backend.Models
{
    public class Canzone : IIdentifiable
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "il nome è obbligatorio")]
        [StringLength(20, MinimumLength = 3, ErrorMessage = "il nome non può superare i 20 caratteri")]
        public string Nome { get; set; }

        [Required(ErrorMessage = "la durata è obbligatoria")]
        [StringLength(6, MinimumLength = 1, ErrorMessage = "la durata deve avere da 1 a 6 caratteri")]
        [RegularExpression(@"^\d{2}\:\d{2}$", ErrorMessage = "la durata deve avere il formato XX:XX")]
        public string Durata { get; set; }
    }
}