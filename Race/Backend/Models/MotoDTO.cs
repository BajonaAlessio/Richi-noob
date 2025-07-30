using Backend.Utils;
namespace Backend.Models
{
    public class MotoDTO : IIdentifiable
    {
        public int Id { get; set; }
        public string Marca { get; set; }
        public string Modello { get; set; }
        public int Potenza { get; set; }
         public int Grip { get; set; }
        public int Inclinazione { get; set; }
        public int VelocitaMassima { get; set; }
        public int Accelerazione { get; set; }
    
    }
}