using System.Collections.Generic;

namespace WebAPI.Models
{
    public class AvailabilityRequestModelForLandlord
    {
        public int Id { get; set; }
        public string AdvertName { get; set; }
        public string Address { get; set; }
        public string Phone { get; set; }
        public string CustomerName { get; set; }
        public int UserId { get; set; }
        public List<AvailableTimeModel> AvailableTimeModels { get; set; }
    }
}
