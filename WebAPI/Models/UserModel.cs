using System;

namespace WebAPI.Models
{
    public class UserModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }
        public DateTime Created { get; set; }
        public string Phone { get; set; }
    }
}
