using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Advert
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int Price { get; set; }
        public int UserId { get; set; }
        public List<Image> Images { get; set; }
        public List<AvailableTime> AvailableTimes { get; set; }
        public List<Review> Reviews { get; set; }
    }
}
