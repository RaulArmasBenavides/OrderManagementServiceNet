using TApiPeliculas.Core.Entities;
using TApiPeliculas.Infraestructure.Repository.IRepository;

namespace TApiPeliculas.Core.IRepository
{
    public interface ICategoriaRepositorio : IRepository<Categoria>
    {
        ICollection<Categoria> GetCategorias();
        Categoria GetCategoria(int CategoriaId);
        bool CrearCategoria(Categoria categoria);
        bool ActualizarCategoria(Categoria categoria);
        bool BorrarCategoria(Categoria categoria);
        bool Guardar();
    }
}
