using System.Collections.Generic;

namespace WebAPI.Models
{
    public class AdvertModelForRequest
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<ImageModel> Images { get; set; }
    }
}
