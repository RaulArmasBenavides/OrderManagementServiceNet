

using TApiPeliculas.Core.Entities;

namespace TApiPeliculas.Infraestructure.Repository.IRepository
{
    public interface IUsuarioRepositorio
    {
        ICollection<Usuario> GetUsuarios();
        Usuario GetUsuario(int usuarioId);
        bool IsUniqueUser(string usuario);
        //Task<UsuarioLoginRespuestaDto> Login(UsuarioLoginDto usuarioLoginDto);
        //Task<UsuarioDatosDto> Registro(UsuarioRegistroDto usuarioRegistroDto);
        AppUsuario GetUsuarioByUserName(string userName);
    }
}
