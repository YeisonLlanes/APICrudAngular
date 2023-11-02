using System.ComponentModel.DataAnnotations;

namespace ApiCrudAngular.Models
{
    public class Departamento
    {
         public int idDepartamento { get; set; }

        [Required(ErrorMessage = "* Oblligatorio")]
        public string? descripcion { get; set;}
    }
}
