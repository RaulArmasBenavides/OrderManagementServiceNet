using Microsoft.AspNetCore.Identity;

namespace TApiPeliculas.Core.Entities
{
    public class AppUsuario : IdentityUser
    {
        public string Nombre { get; set; }
    }
}
