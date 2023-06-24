using OrderMngmt.Data.Models;
using Microsoft.AspNetCore.Identity;

namespace OrderMngmt.Data.Interfaces
{
    public interface IUserAuthenticationRepository : IRepository<User>
    {
        Task<IdentityResult> RegisterUser(User user);
        Task<bool> ValidateUser(User user, string password);
        Task<string> GenerateToken(User user);
        Task<User> GetByUsername(string userName);
        Task<IList<string>> GetRoles(User user);
    }
}