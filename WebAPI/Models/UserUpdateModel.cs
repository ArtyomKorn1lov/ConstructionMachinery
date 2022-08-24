using System.ComponentModel.DataAnnotations;

namespace WebAPI.Models
{
    public class UserUpdateModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }

        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}
