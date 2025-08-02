using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Product.Application.DTOs;
using Product.Application.Interface;
using Product.Domain;
using Product.Infra.Interface;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace Product.Application.Service
{
    public class AuthService : IAuthService
    {
        private readonly IUserRepository _userRepository;
        private readonly IConfiguration _configuration;

        public AuthService(IUserRepository userRepository, IConfiguration configuration)
        {
            _userRepository = userRepository;
            _configuration = configuration;
        }

        public async Task<AuthResponse> LoginAsync(LoginRequest request)
        {
            var user = await _userRepository.GetByUsernameAsync(request.Username);
            var hashedPassword = HashPassword(request.Password);

            if (user == null || hashedPassword != user.PasswordHash)
            {
                throw new UnauthorizedAccessException("Invalid username or password");
            }

            var token = GenerateJwtToken(user);
            
            return new AuthResponse
            {
                Token = token,
                Username = user.Username,
                Role = user.Role,
                ExpiresAt = DateTime.UtcNow.AddHours(1)
            };
        }

        public async Task<AuthResponse> RegisterAsync(RegisterRequest request)
        {            
            var existingUser = await _userRepository.GetByUsernameAsync(request.Username);
            if (existingUser != null)
            {
                throw new InvalidOperationException("Username already exists");
            }

            var passwordHash = HashPassword(request.Password);
            
            var user = new User
            {
                Username = request.Username,
                PasswordHash = passwordHash,
                Role = "User",
                IsActive = true
            };

            await _userRepository.AddAsync(user);
            
            var token = GenerateJwtToken(user);
            
            return new AuthResponse
            {
                Token = token,
                Username = user.Username,
                Role = user.Role,
                ExpiresAt = DateTime.UtcNow.AddHours(1)
            };
        }

        public string GenerateJwtToken(User user)
        {
            var jwtKey = _configuration["Jwt:Key"];

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtKey));
            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var claims = new[]
            {
                    new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                    new Claim(ClaimTypes.Name, user.Username),
                    new Claim(ClaimTypes.Role, user.Role)
                };

            var token = new JwtSecurityToken(
                claims: claims,
                expires: DateTime.UtcNow.AddHours(1),
                signingCredentials: credentials
            );

            var tokenString = new JwtSecurityTokenHandler().WriteToken(token);

            return tokenString;
        }

        private string HashPassword(string password)
        {
            using var sha256 = SHA256.Create();
            var hashedBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));
            return Convert.ToBase64String(hashedBytes);
        }
    }
} 