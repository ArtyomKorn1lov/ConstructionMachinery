using System;

namespace WebAPI.Models
{
    public class AvailableTimeModel
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public int AdvertId { get; set; }
        public int AvailabilityStateId { get; set; }
    }
}
