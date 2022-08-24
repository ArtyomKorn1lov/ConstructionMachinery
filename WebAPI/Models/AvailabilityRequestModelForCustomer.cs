namespace WebAPI.Models
{
    public class AvailabilityRequestModelForCustomer
    {
        public int Id { get; set; }
        public string Address { get; set; }
        public string LandlordName { get; set; }
        public string Phone { get; set; }
        public int RequestStateId { get; set; }
        public int UserId { get; set; }
        public List<AvailableTimeModel> AvailableTimeModels { get; set; }
    }
}
