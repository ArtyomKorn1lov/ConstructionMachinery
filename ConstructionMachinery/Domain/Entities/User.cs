using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class User
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Password { get; set; }
        public List<Advert> Adverts { get; set; }
        public List<AvailabilityRequest> AvailabilityRequests { get; set; }
        public List<Review> Reviews { get; set; }
    }
}
