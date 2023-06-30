using System.ComponentModel.DataAnnotations;

namespace TApiPeliculas.Modelos.Dtos
{
    public class CrearCategoriaDto
    {        
        //Esta validación es importante sino se crear vacía el nombre de categoría
        [Required(ErrorMessage = "El nombre es obligatorio")]
        [MaxLength(60, ErrorMessage = "El número máximo de caracteres es de 60!")]
        public string Nombre { get; set; }        
    }
}
