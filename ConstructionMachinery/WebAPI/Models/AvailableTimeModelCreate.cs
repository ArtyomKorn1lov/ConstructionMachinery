namespace WebAPI.Models
{
    public class AvailableTimeModelCreate
    {
        public DateTime Date { get; set; }
        public int AdvertId { get; set; }
        public int AvailabilityStateId { get; set; }
    }
}
