using System.ComponentModel.DataAnnotations;

namespace Backend.Models
{
    public class Indirizzo
    {
        [Required(ErrorMessage = "via obbligatoria")]
        [StringLength(20, MinimumLength = 3, ErrorMessage = "la via non può superare i 20 caratteri")]
        public string Via { get; set; }

        [Required(ErrorMessage = "città è obbligatoria")]
        [StringLength(20, MinimumLength = 3, ErrorMessage = "la città non può superare i 20 caratteri")]
        public string Citta { get; set; }

        [Required(ErrorMessage = "il CAP è obbligatorio")]
        [StringLength(5, ErrorMessage = "il CAP non può superare i 5 caratteri")]
        //[RegularExpression(@"^d{5}$", ErrorMessage = "Il CAP deve essere di 5 cifre")]
        //[DataType(DataType.PostalCode)]
        public string Cap { get; set; }
    }
}