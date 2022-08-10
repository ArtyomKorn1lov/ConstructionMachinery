using System.ComponentModel.DataAnnotations;

namespace WebAPI.Models
{
    public class RegisterModel
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }

        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}
