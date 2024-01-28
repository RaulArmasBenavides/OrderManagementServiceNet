using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TApiPeliculas.Application.Interfaces;
using TApiPeliculas.Core.Entities;
using TApiPeliculas.Infraestructure.Repository.UnitOfWork;

namespace TApiPeliculas.Application.Services
{
    public class CategoriaService : ICategoriaService
    {

        private readonly IUnitOfWork _contenedorTrabajo;
        private readonly IMapper _mapper;

        public CategoriaService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _contenedorTrabajo = unitOfWork;
            _mapper = mapper;
        }
        public async Task CreateCategoryAsync(Categoria cat)
        {
            _contenedorTrabajo.Categorias.Add(cat);
            await _contenedorTrabajo.SaveChangesAsync();
        }

        public async Task DeleteCategoryAsync(int id)
        {
            _contenedorTrabajo.Categorias.Remove(id);
            await _contenedorTrabajo.SaveChangesAsync();
        }

        public async Task UpdateCategoryAsync(Categoria cat)
        {
            _contenedorTrabajo.Categorias.Update(cat);
            await _contenedorTrabajo.SaveChangesAsync();
        }

        public IEnumerable<object> GetAllCategories()
        {
            throw new NotImplementedException();
        }
    }
}
