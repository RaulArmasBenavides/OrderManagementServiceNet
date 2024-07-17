using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TApiPeliculas.Application.Dtos;
using TApiPeliculas.Core.Entities;

namespace TApiPeliculas.Application.Interfaces
{
    public interface IUsuarioService
    {
        Task<UsuarioDatosDto> Registro(UsuarioRegistroDto usuarioRegistroDto);

        Task<UsuarioLoginRespuestaDto> Login(UsuarioLoginDto usuarioLoginDto, string SecretKey);

        ICollection<AppUsuario> GetUsuarios();

        AppUsuario GetUsuario(string id);
    }
}
