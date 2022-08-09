using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class AvailableTime
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public int AvailabilityRequestId { get; set; }
        public int AdvertId { get; set; }
        public int AvailabilityStateId { get; set; }
    }
}
