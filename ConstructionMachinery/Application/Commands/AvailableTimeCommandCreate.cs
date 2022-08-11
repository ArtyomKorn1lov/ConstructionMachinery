using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Commands
{
    public class AvailableTimeCommandCreate
    {
        public DateTime Date { get; set; }
        public int AvailabilityStateId { get; set; }
    }
}
