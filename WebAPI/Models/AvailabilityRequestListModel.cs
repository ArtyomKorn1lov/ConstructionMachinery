using System;
using System.Collections.Generic;
using WebAPI.Models;

namespace WebApi.Models
{
    public class AvailabilityRequestListModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime Date { get; set; }
        public List<ImageModel> Images { get; set; }
    }
}
