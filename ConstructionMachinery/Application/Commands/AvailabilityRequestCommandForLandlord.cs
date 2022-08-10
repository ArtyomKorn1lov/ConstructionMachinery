using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Commands
{
    public class AvailabilityRequestCommandForLandlord
    {
        public int Id { get; set; }
        public string Address { get; set; }
        public int UserId { get; set; }
        public string Phone { get; set; }
        public List<AvailableTimeCommand> AvailableTimeCommands { get; set; }
    }
}
