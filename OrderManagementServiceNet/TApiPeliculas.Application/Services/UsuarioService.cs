using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using OrderManagementService.Application.Dtos;
using OrderManagementService.Application.Interfaces;
using OrderManagementService.Core.Entities;
using OrderManagementService.Infrastructure.Repository.UnitOfWork;

namespace OrderManagementService.Application.Services
{
    public class UserService : IUserService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly UserManager<AppUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public UserService(IUnitOfWork unitOfWork, IMapper mapper,
            UserManager<AppUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _userManager = userManager;
            _roleManager = roleManager;
        }

        public async Task<ICollection<UserDto>> GetUsersAsync()
        {
            var users = await _unitOfWork.Users.GetUsersAsync();
            return _mapper.Map<ICollection<UserDto>>(users);
        }

        public async Task<UserDto?> GetUserAsync(string id)
        {
            var user = await _unitOfWork.Users.GetUserAsync(id);
            return user == null ? null : _mapper.Map<UserDto>(user);
        }

        public async Task<LoginResponseDto> LoginAsync(LoginDto dto, string secretKey)
        {
            var user = await _unitOfWork.Users.GetUserByUsernameAsync(dto.Username.ToLower());
            if (user == null)
                return new LoginResponseDto { Token = string.Empty };

            bool isValid = await _userManager.CheckPasswordAsync(user, dto.Password);
            if (!isValid)
                return new LoginResponseDto { Token = string.Empty };

            var roles = await _userManager.GetRolesAsync(user);
            var key = Encoding.ASCII.GetBytes(secretKey);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new(ClaimTypes.NameIdentifier, user.Id),
                    new(ClaimTypes.Name, user.UserName!),
                    new(ClaimTypes.Role, roles.FirstOrDefault() ?? string.Empty)
                }),
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = new(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var handler = new JwtSecurityTokenHandler();
            var token = handler.CreateToken(tokenDescriptor);

            return new LoginResponseDto
            {
                Token = handler.WriteToken(token),
                Role = roles.FirstOrDefault() ?? string.Empty,
                User = _mapper.Map<UserDto>(user)
            };
        }

        public async Task<UserDto?> RegisterAsync(RegisterDto dto)
        {
            var user = new AppUser
            {
                UserName = dto.Username.ToLower(),
                Email = dto.Username,
                NormalizedEmail = dto.Username.ToUpper(),
                FullName = dto.FullName
            };

            var result = await _userManager.CreateAsync(user, dto.Password);
            if (!result.Succeeded)
                return null;

            if (!await _roleManager.RoleExistsAsync("admin"))
            {
                await _roleManager.CreateAsync(new IdentityRole("admin"));
                await _roleManager.CreateAsync(new IdentityRole("registered"));
            }

            var role = string.IsNullOrWhiteSpace(dto.Role) ? "registered" : dto.Role;
            await _userManager.AddToRoleAsync(user, role);

            var created = await _unitOfWork.Users.GetUserByUsernameAsync(dto.Username.ToLower());
            return created == null ? null : _mapper.Map<UserDto>(created);
        }
    }
}
