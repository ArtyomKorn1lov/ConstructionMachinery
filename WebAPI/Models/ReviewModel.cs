using System;

namespace WebAPI.Models
{
    public class ReviewModel
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public DateTime Date { get; set; }
        public int ReviewStateId { get; set; }
    }
}
