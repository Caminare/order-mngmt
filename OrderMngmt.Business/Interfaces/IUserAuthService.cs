using Microsoft.AspNetCore.Identity;
using OrderMngmt.Business.Models;


namespace OrderMngmt.Business.Interfaces
{
    public interface IUserAuthService
    {
        Task<IdentityResult> RegisterAsync(UserRegisterDTO customer);
        Task<bool> ValidateUserAsync(UserLoginDTO userDto);
        Task<string> CreateTokenAsync();

    }
}