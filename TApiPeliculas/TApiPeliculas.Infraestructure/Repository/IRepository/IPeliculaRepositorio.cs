

using TApiPeliculas.Core.Entities;

namespace TApiPeliculas.Infraestructure.Repository.IRepository
{
    public interface IPeliculaRepositorio : IRepository<Pelicula>
    {
        ICollection<Pelicula> GetPeliculas();
        ICollection<Pelicula> GetPeliculasEnCategoria(int CatId);
        Pelicula GetPelicula(int PeliculaId);
        IEnumerable<Pelicula> BuscarPelicula(string nombre);
        bool CrearPelicula(Pelicula pelicula);
        bool ActualizarPelicula(Pelicula pelicula);
        bool BorrarPelicula(Pelicula pelicula);
        bool Guardar();
    }
}
