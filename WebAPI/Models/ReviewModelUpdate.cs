using System;

namespace WebAPI.Models
{
    public class ReviewModelUpdate
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public DateTime Date { get; set; }
        public int ReviewStateId { get; set; }
        public int AdvertId { get; set; }
        public int UserId { get; set; }
    }
}
