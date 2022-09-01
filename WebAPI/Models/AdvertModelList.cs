using System.Collections.Generic;

namespace WebAPI.Models
{
    public class AdvertModelList
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Price { get; set; }
        public List<ImageModel> Images { get; set; }
    }
}
