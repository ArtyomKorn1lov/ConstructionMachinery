using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Review
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public DateTime Date { get; set; }
        public int AdvertId { get; set; }
        public int UserId { get; set; }
        public int ReviewStateId { get; set; }

        public void CopyFrom(Review review)
        {
            Description = review.Description;
            Date = review.Date;
            ReviewStateId = review.ReviewStateId;
            UserId = review.UserId;
            AdvertId = review.ReviewStateId;
        }
    }
}
