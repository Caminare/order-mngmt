using System.ComponentModel.DataAnnotations;
using OrderMngmt.Data.Models;

namespace OrderMngmt.Business.Models
{
    public class UserRegisterDTO
    {
        public string? Email { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }

                
        [Required(ErrorMessage = "Password is required")]
        public string? Password { get; set; }

        [Required(ErrorMessage = "Username is required")]
        public string? UserName { get; set; }

        public User ToEntity()
        {
            return new User
            {
                Email = Email,
                FirstName = FirstName,
                LastName = LastName,
                PasswordHash = Password,
                UserName = UserName
            };
        }
    }
}