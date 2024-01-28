using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using TApiPeliculas.Application.Dtos;
using TApiPeliculas.Application.Interfaces;
using TApiPeliculas.Core.Entities;
using TApiPeliculas.Infraestructure.Repository.UnitOfWork;
namespace TApiPeliculas.Application.Services
{
    public class UsuarioService : IUsuarioService
    {
        private readonly IMapper _mapper;
        private readonly UserManager<AppUsuario> _userManager;
        private readonly IUnitOfWork _contenedorTrabajo;
        private IConfiguration _config;

        private readonly RoleManager<IdentityRole> _roleManager;
        public UsuarioService(IUnitOfWork unitOfWork, IConfiguration config, UserManager<AppUsuario> userManager, IMapper mapper, RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager;

            _mapper = mapper;
            _contenedorTrabajo = unitOfWork;
            _userManager = userManager;
            _config = config;
            _roleManager = roleManager;
        }
        public AppUsuario GetUsuario(string id)
        {
            throw new NotImplementedException();
        }

        public ICollection<AppUsuario> GetUsuarios()
        {
            throw new NotImplementedException();
        }

        public async Task<UsuarioLoginRespuestaDto> Login(UsuarioLoginDto usuarioLoginDto, string SecretKey)
        {
            var usuario = _contenedorTrabajo.Usuarios.GetUsuarioByUserName(usuarioLoginDto.NombreUsuario.ToLower());
            bool isValid = await _userManager.CheckPasswordAsync(usuario, usuarioLoginDto.Password);
            //Validamos si el usuario no existe con la combinación de usuario y contraseña correcta
            if (usuario == null || !isValid )
            {
                //return null;
                return new UsuarioLoginRespuestaDto()
                {
                    Token = "",
                    Usuario = null
                };
            }
            //Aquí existe el usuario entonces podemos procesar el login
            var roles = await _userManager.GetRolesAsync(usuario);
            var manejadorToken = new JwtSecurityTokenHandler();


            var key = Encoding.ASCII.GetBytes(SecretKey);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new(ClaimTypes.Name, usuario.UserName.ToString()),
                    new(ClaimTypes.Role, roles.FirstOrDefault())
                }),
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = new(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = manejadorToken.CreateToken(tokenDescriptor);
            UsuarioLoginRespuestaDto usuarioLoginRespuestaDto = new UsuarioLoginRespuestaDto()
            {
                Token = manejadorToken.WriteToken(token),
                Usuario = _mapper.Map<UsuarioDatosDto>(usuario),

            };
            return usuarioLoginRespuestaDto;
        }

        public Task<UsuarioDatosDto> Registro(UsuarioRegistroDto usuarioRegistroDto)
        {
            throw new NotImplementedException();
        }
    }
}
