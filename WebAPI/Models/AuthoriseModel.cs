namespace WebAPI.Models
{
    public class AuthorizeModel
    {
        public string Name { get; set; }
        public string Email { get; set; }

        public AuthorizeModel(string name, string email)
        {
            Name = name;
            Email = email;
        }
    }
}
