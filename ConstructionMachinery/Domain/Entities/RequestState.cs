using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class RequestState
    {
        public int Id { get; set; }
        public string State { get; set; }
        public List<AvailabilityRequest> AvailabilityRequests { get; set; }
    }
}
