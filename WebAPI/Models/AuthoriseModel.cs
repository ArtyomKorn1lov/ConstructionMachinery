namespace WebAPI.Models
{
    public class AuthorizeModel
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public bool IsAvailable { get; set; }

        public AuthorizeModel(string name, string email, bool isAvailable)
        {
            Name = name;
            Email = email;
            IsAvailable = isAvailable;
        }
    }
}
