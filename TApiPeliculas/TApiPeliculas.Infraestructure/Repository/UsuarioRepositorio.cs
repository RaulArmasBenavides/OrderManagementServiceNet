using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using System.Data;
using TApiPeliculas.Core.Entities;
using TApiPeliculas.Infraestructure.Repository.Data;
using TApiPeliculas.Infraestructure.Repository.IRepository;

namespace TApiPeliculas.Repositorio
{
    public class UsuarioRepositorio : IUsuarioRepositorio
    {
        private readonly ApplicationDbContext _bd;
 
 
 
        public UsuarioRepositorio(ApplicationDbContext bd, IConfiguration config)
        {
            _bd = bd;
            
            
          
        }

        public Usuario GetUsuario(int usuarioId)
        {
            return _bd.Usuario.FirstOrDefault(c => c.Id == usuarioId);
        }

        public AppUsuario GetUsuarioByUserName(string userName)
        {
            return _bd.AppUsuario.FirstOrDefault(u => u.UserName == userName);
        }


        public ICollection<Usuario> GetUsuarios()
        {
            return _bd.Usuario.OrderBy(c => c.NombreUsuario).ToList();
        }



        public bool IsUniqueUser(string usuario)
        {
            var usuariobd = _bd.AppUsuario.FirstOrDefault(u => u.UserName== usuario);
            if (usuariobd == null)
            {
                return true;
            }
            return false;
        }

        

 
    }
}
