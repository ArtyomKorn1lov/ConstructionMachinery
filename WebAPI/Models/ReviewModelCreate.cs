using System;

namespace WebAPI.Models
{
    public class ReviewModelCreate
    {
        public string Description { get; set; }
        public DateTime Date { get; set; }
        public int ReviewStateId { get; set; }
        public int AdvertId { get; set; }
        public int UserId { get; set; }
    }
}
