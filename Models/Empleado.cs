using System.ComponentModel.DataAnnotations;

namespace ApiCrudAngular.Models
{
    public class Empleado
    {
        public int idEmpleado { get; set; }

        [Required(ErrorMessage = "* Obligatorio")]
        public string Nombre { get; set; } = null!;

        [Required]
        [Range(1, int.MaxValue, ErrorMessage = "* Obligatorio")]
        public int idDepartamento { get; set; }

        [Required(ErrorMessage ="* Obligatorio")]
        public int Sueldo { get; set; }

        [Required(ErrorMessage ="* Obligatorio")]
        public DateTime FechaIngreso { get; set; }

    }
}
