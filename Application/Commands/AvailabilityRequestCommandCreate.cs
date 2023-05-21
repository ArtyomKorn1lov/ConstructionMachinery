using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Commands
{
    public class AvailabilityRequestCommandCreate
    {
        public string Address { get; set; }
        public string Conditions { get; set; }
        public int RequestStateId { get; set; }
        public int UserId { get; set; }
        public List<AvailableTimeCommandForCreateRequest> AvailableTimeCommandForCreateRequests { get; set; }
    }
}
