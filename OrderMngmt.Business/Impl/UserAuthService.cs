using OrderMngmt.Business.Interfaces;
using OrderMngmt.Data.Interfaces;
using Microsoft.AspNetCore.Identity;
using System.IdentityModel.Tokens.Jwt;
using System.Text;
using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;
using Microsoft.Extensions.Configuration;
using System.Threading.Tasks;
using OrderMngmt.Business.Models;
using OrderMngmt.Data.Models;

namespace OrderMngmt.Business.Impl
{
    public class UserAuthService : IUserAuthService
    {
        private readonly IUnitOfWork _unitOfWork;
        private User? _user;
        private IConfigurationSection _jwtSettings;
        private readonly IConfiguration _configuration;

        public UserAuthService(IUnitOfWork unitOfWork, IConfiguration configuration)
        {
            _unitOfWork = unitOfWork;
            _configuration = configuration;
        }
        public async Task<IdentityResult> RegisterAsync(UserRegisterDTO userDto)
        {
            var user = userDto.ToEntity();
            var result = await _unitOfWork.GetUserAuthenticationRepository().RegisterUser(user);
            await _unitOfWork.SaveChanges();
            return result;
        }

        public async Task<bool> ValidateUserAsync(UserLoginDTO userDto)
        {
            _user = await _unitOfWork.GetUserAuthenticationRepository().GetByUsername(userDto.UserName);
            var result = await _unitOfWork.GetUserAuthenticationRepository().ValidateUser(_user, userDto.Password);
            return result;
        }

        public async Task<string> CreateTokenAsync()
        {
            _jwtSettings = _configuration.GetSection("JwtConfig");
            var signingCredentials = GetSigningCredentials();
            var claims = await GetClaims();
            var tokenOptions = GenerateTokenOptions(signingCredentials, claims);
            return new JwtSecurityTokenHandler().WriteToken(tokenOptions);
        }

        private SigningCredentials GetSigningCredentials()
        {
            var key = Encoding.UTF8.GetBytes(_jwtSettings["Secret"]);
            var secret = new SymmetricSecurityKey(key);
            return new SigningCredentials(secret, SecurityAlgorithms.HmacSha256);
        }

        private async Task<List<Claim>> GetClaims()
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, _user.UserName)
            };
            var roles = await _unitOfWork.GetUserAuthenticationRepository().GetRoles(_user);
            foreach (var role in roles)
            {
                claims.Add(new Claim(ClaimTypes.Role, role));
            }
            return claims;
        }

        private JwtSecurityToken GenerateTokenOptions(SigningCredentials signingCredentials, List<Claim> claims)
        {
            var expiry = DateTime.Now.AddMinutes(Convert.ToDouble(_jwtSettings["TokenExpiryInMinutes"]));
            var tokenOptions = new JwtSecurityToken
            (
            issuer: _jwtSettings["ValidIssuer"],
            audience: _jwtSettings["ValidAudience"],
            claims: claims,
            expires: DateTime.Now.AddMinutes(Convert.ToDouble(_jwtSettings["TokenExpiryInMinutes"])),
            signingCredentials: signingCredentials
            );
            return tokenOptions;
        }
    }
}