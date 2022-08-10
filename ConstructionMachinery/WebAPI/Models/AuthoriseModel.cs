namespace WebAPI.Models
{
    public class AuthoriseModel
    {
        public string Name { get; set; }
        public string Type { get; set; }

        public AuthoriseModel(string name, string type)
        {
            Name = name;
            Type = type;
        }
    }
}
