using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class AvailabilityRequest
    {
        public int Id { get; set; }
        public string Address { get; set; }
        public bool IsAvailable { get; set; }
        public int RequestStateId { get; set; }
        public int UserId { get; set; }
        public List<AvailableTime> AvailableTimes { get; set; }

        public void CopyFrom(AvailabilityRequest availabilityRequest)
        {
            Address = availabilityRequest.Address;
            RequestStateId = availabilityRequest.RequestStateId;
            UserId = availabilityRequest.UserId;
            AvailableTimes = availabilityRequest.AvailableTimes;
        }
    }
}
