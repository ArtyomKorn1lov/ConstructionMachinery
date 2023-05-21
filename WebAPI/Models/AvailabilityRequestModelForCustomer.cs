using System;
using System.Collections.Generic;

namespace WebAPI.Models
{
    public class AvailabilityRequestModelForCustomer
    {
        public int Id { get; set; }
        public string AdvertName { get; set; }
        public DateTime Created { get; set; }
        public DateTime Updated { get; set; }
        public string Address { get; set; }
        public string Conditions { get; set; }
        public int Sum { get; set; }
        public string LandlordName { get; set; }
        public string Phone { get; set; }
        public int RequestStateId { get; set; }
        public int UserId { get; set; }
        public List<ImageModel> Images { get; set; }
        public DateTime StartRent { get; set; }
        public DateTime EndRent { get; set; }
    }
}
