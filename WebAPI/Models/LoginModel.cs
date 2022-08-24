using System.ComponentModel.DataAnnotations;

namespace WebAPI.Models
{
    public class LoginModel
    {
        public string Email { get; set; }

        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}
