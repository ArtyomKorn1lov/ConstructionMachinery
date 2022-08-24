using System.Collections.Generic;

namespace WebAPI.Models
{
    public class AdvertModelCreate
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public int Price { get; set; }
        public int UserId { get; set; }
        public List<AvailableTimeModelCreate> AvailableTimeModelsCreates { get; set; }
    }
}
