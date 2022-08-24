namespace WebAPI.Models
{
    public class AdvertModelUpdate
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int Price { get; set; }
        public int UserId { get; set; }
        public List<AvailableTimeModel> AvailableTimeModels { get; set; }
    }
}
