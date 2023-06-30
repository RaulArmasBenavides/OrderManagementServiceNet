using Microsoft.AspNetCore.Identity;

namespace TApiPeliculas.Modelos
{
    public class AppUsuario : IdentityUser
    {
        public string Nombre { get; set; }
    }
}
