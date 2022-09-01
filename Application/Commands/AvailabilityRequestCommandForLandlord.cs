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
        public string AdvertName { get; set; }
        public string Address { get; set; }
        public string CustomerName { get; set; }
        public int UserId { get; set; }
        public string Phone { get; set; }
        public List<ImageCommand> Images { get; set; }
        public List<AvailableTimeCommand> AvailableTimeCommands { get; set; }
    }
}
