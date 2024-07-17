using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TApiPeliculas.Core.IRepository;
using TApiPeliculas.Infraestructure.Repository.IRepository;

namespace TApiPeliculas.Infraestructure.Repository.UnitOfWork
{
    public interface IUnitOfWork : IDisposable
    {
       
            ICategoriaRepositorio Categorias { get; }

            IPeliculaRepositorio Peliculas { get; }

            IUsuarioRepositorio Usuarios { get; }

            void Save();

            Task<int> SaveChangesAsync();
        
    }
}
