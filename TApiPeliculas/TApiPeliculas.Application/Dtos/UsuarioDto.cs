using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TApiPeliculas.Application.Dtos
{
    public class UsuarioDto
    {
        public int Id { get; set; }
        public string NombreUsuario { get; set; }
        public string Nombre { get; set; }
        public string Password { get; set; }
        public string Role { get; set; }
    }
}
