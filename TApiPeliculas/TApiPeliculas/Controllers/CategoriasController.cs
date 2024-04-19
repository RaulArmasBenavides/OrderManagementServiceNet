using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TApiPeliculas.Application.Dtos;
using TApiPeliculas.Application.Interfaces;
using TApiPeliculas.Core.Entities;

namespace TApiPeliculas.Controllers
{
    [Route("api/Categorias")]
    //[Route("api/[controller]")] //Otra forma aunque no muy recomendada
    [ApiController]  
    //[ApiExplorerSettings(GroupName = "ApiPeliculasCategorias")]
    //[ProducesResponseType(StatusCodes.Status400BadRequest)]
    public class CategoriasController : ControllerBase
    {

        private readonly ICategoriaService _ctCategoriaService;
        private readonly IMapper _mapper;

        public CategoriasController(ICategoriaService ctRepo, IMapper mapper)
        {
            _ctCategoriaService = ctRepo;
            _mapper = mapper;
        }

        [AllowAnonymous]
        [HttpGet]
        //[ResponseCache(Duration = 20)]
        //Opción dos usando el profile
        [ResponseCache(CacheProfileName = "PorDefecto20Segundos")]      
        [ProducesResponseType(StatusCodes.Status403Forbidden)]       
        [ProducesResponseType(StatusCodes.Status200OK)]
        public IActionResult GetCategorias()
        {
            var listaCategorias = _ctCategoriaService.GetAllCategories();

            var listaCategoriasDto = new List<CategoriaDto>();

            foreach (var lista in listaCategorias)
            {
                listaCategoriasDto.Add(_mapper.Map<CategoriaDto>(lista));
            }
            return Ok(listaCategoriasDto);
        }

        
        [AllowAnonymous]
        //1-
        //[ResponseCache(Duration = 40)]
        //2- Existen otros parámetros para evitar la cache
        //Con este se evita la cache y el no store se usa en true para evitar que se 
        //guarden los errores en cache y que se estén mostrando
        //En pocas palabras queda sin cache y siempre se hace una petición al servidor 
        //nueva
        [ResponseCache(Location = ResponseCacheLocation.None, NoStore = true)]
        [HttpGet("{categoriaId:int}", Name = "GetCategoria")]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]      
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult GetCategoria(int categoriaId)
        {
            var itemCategoria = _ctCategoriaService.GetCategoria(categoriaId);

            if (itemCategoria == null)
            {
                return NotFound();
            }

            var itemCategoriaDto = _mapper.Map<CategoriaDto>(itemCategoria);
            return Ok(itemCategoriaDto);
        }

        [Authorize(Roles = "admin")]
        [HttpPost]
        [ProducesResponseType(201, Type = typeof(CategoriaDto))]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> CrearCategoria([FromBody] CrearCategoriaDto crearCategoriaDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            if (crearCategoriaDto == null)
            {
                return BadRequest(ModelState);
            }

            //if (_ctCategoriaService.ExisteCategoria(crearCategoriaDto.Nombre))
            //{
            //    ModelState.AddModelError("", "La categoría ya existe");
            //    return StatusCode(404, ModelState);
            //}

            var categoria = _mapper.Map<Categoria>(crearCategoriaDto);
            await _ctCategoriaService.CreateCategoryAsync(categoria);
            //if (!await _ctCategoriaService.CreateCategoryAsync(categoria))
            //{
            //    ModelState.AddModelError("", $"Algo salio mal guardando el registro{categoria.Nombre}");
            //    return StatusCode(500, ModelState);
            //}

            return CreatedAtRoute("GetCategoria", new { categoriaId = categoria.Id }, categoria);
        }


        [Authorize(Roles = "admin")]
        [HttpPatch("{categoriaId:int}", Name = "ActualizarPatchCategoria")]        
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public IActionResult ActualizarPatchCategoria(int categoriaId, [FromBody] CategoriaDto categoriaDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (categoriaDto == null || categoriaId != categoriaDto.Id)
            {
                return BadRequest(ModelState);
            }

            var categoria = _mapper.Map<Categoria>(categoriaDto);
            _ctCategoriaService.UpdateCategoryAsync(categoria);
            //if (!_ctCategoriaService.UpdateCategoryAsync(categoria))
            //{
            //    ModelState.AddModelError("", $"Algo salio mal actualizando el registro{categoria.Nombre}");
            //    return StatusCode(500, ModelState);
            //}

            return NoContent();
        }

        //Aquí se prueba el rol con uno que no existe a persar de autenticarse bien no puede
        //borrar (Con este método se prueba). El rol está asignado al usuario
        [Authorize(Roles = "TESTROLNOEXISTENTE")]
        [HttpDelete("{categoriaId:int}", Name = "BorrarCategoria")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult BorrarCategoria(int categoriaId)
        {
            //if (!_ctRepo.ExisteCategoria(categoriaId))
            //{
            //    return NotFound();
            //}

            var categoria = _ctCategoriaService.GetCategoria(categoriaId);
            _ctCategoriaService.DeleteCategoryAsync(categoria.Id);
            //if (!)
            //{
            //    ModelState.AddModelError("", $"Algo salio mal borrando el registro{categoria.Nombre}");
            //    return StatusCode(500, ModelState);
            //}

            return NoContent();
        }
    }
}
