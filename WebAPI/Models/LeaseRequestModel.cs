using System.Collections.Generic;

namespace WebAPI.Models
{
    public class LeaseRequestModel
    {
        public int Price { get; set; }
        public List<AvailableDayModel> AvailableDayModels { get; set; }
    }
}
