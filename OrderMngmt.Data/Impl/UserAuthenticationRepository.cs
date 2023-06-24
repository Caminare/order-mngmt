using OrderMngmt.Data.Interfaces;
using OrderMngmt.Data.Models;
using Microsoft.AspNetCore.Identity;

namespace OrderMngmt.Data.Impl
{
    public class UserAuthenticationRepository : Repository<User>, IUserAuthenticationRepository 
    {
        private readonly UserManager<User> _userManager;

        public UserAuthenticationRepository(OrderMngmtDbContext context, UserManager<User> userManager) : base(context)
        {
            _userManager = userManager;
        }
        public async Task<IdentityResult> RegisterUser(User user)
        {
            var result = await _userManager.CreateAsync(user, user.PasswordHash);
            return result;
        }
        public async Task<bool> ValidateUser(User user, string password)
        {
            var result = await _userManager.CheckPasswordAsync(user, password);
            return result;
        }
        public async Task<string> GenerateToken(User user)
        {
            var token = await _userManager.GeneratePasswordResetTokenAsync(user);
            return token;
        }

        public async Task<User> GetByUsername(string userName)
        {
            var user = await _userManager.FindByNameAsync(userName);
            return user;
        }

        public async Task<IList<string>> GetRoles(User user)
        {
            var roles = await _userManager.GetRolesAsync(user);
            return roles;
        }
    }
}