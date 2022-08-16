namespace WebAPI.Models
{
    public class AuthoriseModel
    {
        public string Name { get; set; }

        public AuthoriseModel(string name)
        {
            Name = name;
        }
    }
}
