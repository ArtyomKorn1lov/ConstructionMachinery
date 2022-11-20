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
        public DateTime DateIssue { get; set; }
        public string PTS { get; set; }
        public string VIN { get; set; }
        public string Description { get; set; }
        public int Price { get; set; }
        public DateTime PublishDate { get; set; }
        public DateTime EditDate { get; set; }
        public int UserId { get; set; }
        public List<Image> Images { get; set; }
        public List<AvailableTime> AvailableTimes { get; set; }
        public List<Review> Reviews { get; set; }

        public void CopyFrom(Advert advert)
        {
            Name = advert.Name;
            DateIssue = advert.DateIssue;
            PTS = advert.PTS;
            VIN = advert.VIN;
            Description = advert.Description;
            EditDate = advert.EditDate;
            Price = advert.Price;
            AvailableTimes = advert.AvailableTimes;
            Reviews = advert.Reviews;
        }
    }
}
