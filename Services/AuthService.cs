using AutoMapper;

using StoreControlAPI.Models;
using StoreControlAPI.DTOs;

namespace StoreControlAPI.Services
{
    public class AuthService : IAuthService
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public AuthService(AppDbContext context, IMapper mapper, IHttpContextAccessor httpContextAccessor)
        {
            _context = context;
            _mapper = mapper;
            _httpContextAccessor = httpContextAccessor;
        }

        public ResultDto Register(RegisterDto registerDto)
        {
            var user = _mapper.Map<User>(registerDto);

            if (user == null)
            {
                return new ResultDto()
                {
                    Success = false,
                    Message = "Введено не вірні дані!"
                };
            }

            if (_context.Users.Any(u => u.Username == user.Username))
            {
                return new ResultDto()
                {
                    Success = false,
                    Message = "Користувача з таким Username вже зареєстровано!"
                };
            }

            user.PasswordHash = BCrypt.Net.BCrypt.HashPassword(registerDto.Password);

            _context.Users.Add(user);
            _context.SaveChanges();

            return new ResultDto()
            {
                Success = true,
                Message = "Реєстрація пройшла успішно!"
            };
        }

        public ResultDto Login(LoginDto loginDto)
        {
            var user = _context.Users.FirstOrDefault(u => u.Username == loginDto.Username.ToLower());
            if (user == null)
            {
                return new ResultDto()
                {
                    Success = false,
                    Message = "Користувача з таким Username не знайдено!"
                };
            }

            if (!BCrypt.Net.BCrypt.Verify(loginDto.Password, user.PasswordHash))
            {
                return new ResultDto()
                {
                    Success = false,
                    Message = "Невірний логін, або пароль!"
                };
            }

            return new ResultDto()
            {
                Success = true,
                Message = "Вхід пройшов успішно!",
                UserId = user.Id
            };
        }

        public bool IsAdmin()
        {
            var userId = _httpContextAccessor.HttpContext?.Session?.GetInt32("UserId");
            if (userId == null) return false;

            var user = _context.Users.FirstOrDefault(u => u.Id == userId);
            if (user == null) return false;

            if (user.Role != Enums.Roles.Admin) return false;

            return true;
        }

        public UserDto? GetCurrentUser(int userId)
        {
            var user = _context.Users.FirstOrDefault(u => u.Id == userId);
            if (user == null) return null;

            UserDto userDto = _mapper.Map<UserDto>(user);

            return userDto;
        }
    }
}
