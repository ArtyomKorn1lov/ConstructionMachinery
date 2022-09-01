using System.Collections.Generic;

namespace WebAPI.Models
{
    public class AdvertModelInfo
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int Price { get; set; }
        public string UserName { get; set; }
        public List<ImageModel> Images { get; set; }
        public List<AvailableTimeModel> AvailableTimes { get; set; }
    }
}
