using System;
using System.Collections.Generic;

namespace WebAPI.Models
{
    public class AvailableDayModel
    {
        public DateTime Date { get; set; }
        public List<AvailableTimeModel> Times { get; set; }
    }
}
