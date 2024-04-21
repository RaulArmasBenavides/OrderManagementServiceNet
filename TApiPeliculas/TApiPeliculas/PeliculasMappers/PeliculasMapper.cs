using AutoMapper;
using TApiPeliculas.Application.Dtos;
using TApiPeliculas.Core.Entities;

namespace TApiPeliculas.PeliculasMappers
{
    public class PeliculasMapper : Profile
    {
        public PeliculasMapper()
        {
            CreateMap<Categoria, CategoriaDto>().ReverseMap();
            CreateMap<Categoria, CrearCategoriaDto>().ReverseMap();
            CreateMap<Pelicula, PeliculaDto>().ReverseMap();
             CreateMap<AppUsuario, UsuarioDto>().ReverseMap();
            CreateMap<AppUsuario, UsuarioDatosDto>().ReverseMap();
            //CreateMap<Pelicula, CrearPeliculaDto>().ReverseMap();
            //CreateMap<Pelicula, PeliculaUpdateDto>().ReverseMap();
        }
    }
}
