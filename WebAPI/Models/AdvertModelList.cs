using System;
using System.Collections.Generic;

namespace WebAPI.Models
{
    public class AdvertModelList
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Price { get; set; }
        public double AverageRating { get; set; }
        public DateTime EditDate { get; set; }
        public List<ImageModel> Images { get; set; }
    }
}
