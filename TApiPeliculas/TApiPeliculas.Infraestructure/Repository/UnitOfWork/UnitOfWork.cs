using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TApiPeliculas.Core.IRepository;
using TApiPeliculas.Infraestructure.Repository.Data;
using TApiPeliculas.Infraestructure.Repository.IRepository;
using TApiPeliculas.Repositorio;

namespace TApiPeliculas.Infraestructure.Repository.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {

        private readonly ApplicationDbContext _db;
        public UnitOfWork(ApplicationDbContext db)
        {
            _db = db;
            Categorias = new CategoriaRepositorio(_db);
            Peliculas = new PeliculaRepositorio(_db);
            Usuarios = new UsuarioRepositorio(_db, null);
        }
        public ICategoriaRepositorio Categorias { get; private set; }
        public IPeliculaRepositorio Peliculas { get; private set; }
        public IUsuarioRepositorio Usuarios { get; private set; }

        public void Dispose()
        {
            _db.Dispose();
        }

        public void Save()
        {
            _db.SaveChanges();
        }

        public Task<int> SaveChangesAsync()
        {
            return _db.SaveChangesAsync();
        }
    }
}
