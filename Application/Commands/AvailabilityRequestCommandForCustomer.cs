using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Commands
{
    public class AvailabilityRequestCommandForCustomer
    {
        public int Id { get; set; }
        public string AdvertName { get; set; }
        public DateTime Created { get; set; }
        public DateTime Updated { get; set; }
        public string Address { get; set; }
        public string Conditions { get; set; }
        public int Sum { get; set; }
        public int RequestStateId { get; set; }
        public string LandlordName { get; set; }
        public int UserId { get; set; }
        public string Phone { get; set; }
        public List<ImageCommand> Images { get; set; }
        public DateTime StartRent { get; set; }
        public DateTime EndRent { get; set; }
    }
}
