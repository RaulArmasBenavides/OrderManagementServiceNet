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

        public AppUsuario GetUsuario(int usuarioId)
        {
            return _bd.AppUsuario.FirstOrDefault(c => c.Id == usuarioId.ToString());
        }

        public AppUsuario GetUsuarioByUserName(string userName)
        {
            return _bd.AppUsuario.FirstOrDefault(u => u.UserName == userName);
        }


        public ICollection<AppUsuario> GetUsuarios()
        {
            return _bd.AppUsuario.OrderBy(c => c.Nombre).ToList();
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
