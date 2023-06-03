using System;
using System.Collections.Generic;

namespace WebAPI.Models
{
    public class FilterModel
    {
        public DateTime StartPublishDate { get; set; }
        public DateTime EndPublishDate { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public int StartTime { get; set; }
        public int EndTime { get; set; }
        public int StartPrice { get; set; }
        public int EndPrice { get; set; }
        public string KeyWord { get; set; }
    }
}
