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
        public string Address { get; set; }
        public DateTime Created { get; set; }
        public string Password { get; set; }
        public string RefreshToken { get; set; }
        public DateTime RefreshTokenExpiryTime { get; set; }
        public List<Advert> Adverts { get; set; }
        public List<AvailabilityRequest> AvailabilityRequests { get; set; }
        public List<Review> Reviews { get; set; }

        public void CopyFrom(User user)
        {
            Name = user.Name;
            Email = user.Email;
            Phone = user.Phone;
            Password = user.Password;
            Adverts = user.Adverts;
            AvailabilityRequests = user.AvailabilityRequests;
            Reviews = user.Reviews;
        }
    }
}
