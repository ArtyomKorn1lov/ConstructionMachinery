using System;
using System.Collections.Generic;

namespace WebAPI.Models
{
    public class FilterModel
    {
        public string StartPublishDate { get; set; }
        public string EndPublishDate { get; set; }
        public string StartDate { get; set; }
        public string EndDate { get; set; }
        public int StartTime { get; set; }
        public int EndTime { get; set; }
        public int StartPrice { get; set; }
        public int EndPrice { get; set; }
        public string KeyWord { get; set; }
    }
}
