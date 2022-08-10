namespace WebAPI.Models
{
    public class AvailabilityRequestModelCreate
    {
        public string Address { get; set; }
        public int RequestStateId { get; set; }
        public int UserId { get; set; }
        public List<AvailableTimeModelForCreateRequest> AvailableTimeModelForCreateRequests { get; set; }
    }
}
