using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace API.Models
{
    [Table("Persona")]
    public class Persona
    {
        [Key]
        public int PersonaId { get; set; }
        public string? Nombre { get; set; }
        public string? Apellido { get; set; }
        public char Sexo { get; set; }
        public DateTime FechaNacimiento { get; set; }
    }
}
