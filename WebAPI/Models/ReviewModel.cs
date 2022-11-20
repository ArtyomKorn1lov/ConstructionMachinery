using System;

namespace WebAPI.Models
{
    public class ReviewModel
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public DateTime Date { get; set; }
        public string Name { get; set; }
        public int UserId  { get; set; }
        public bool IsAuthorized { get; set; }
        public int ReviewStateId { get; set; }
    }
}
