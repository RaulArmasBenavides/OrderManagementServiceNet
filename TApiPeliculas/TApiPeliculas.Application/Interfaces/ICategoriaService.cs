using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TApiPeliculas.Core.Entities;

namespace TApiPeliculas.Application.Interfaces
{
    public interface ICategoriaService
    {
        Task CreateCategoryAsync(Categoria pel);
        Task UpdateCategoryAsync(Categoria pel);
        Task DeleteCategoryAsync(int id);
        Categoria GetCategoria(int id);
        IEnumerable<object> GetAllCategories();
    }
}
