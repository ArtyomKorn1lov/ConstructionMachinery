using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class AvailabilityState
    {
        public int Id { get; set; }
        public string State { get; set; }
        public List<AvailableTime> AvailableTimes { get; set; }
    }
}
