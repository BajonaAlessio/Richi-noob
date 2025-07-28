using Backend.Utils;
using System.ComponentModel.DataAnnotations;

namespace Backend.Models
{
    public class User : IIdentifiable
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "il nome utente è obbligatorio")]
        [StringLength(20, MinimumLength = 3, ErrorMessage = "il nome utente non può superare i 20 caratteri")]
        public string NomeUtente { get; set; }

        [Required(ErrorMessage = "indirizzo è obbligatorio")]
        public Indirizzo Indirizzo { get; set; }
    }
}