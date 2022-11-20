using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Commands
{
    public class AdvertCommandCreate
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime DateIssue { get; set; }
        public string PTS { get; set; }
        public string VIN { get; set; }
        public int Price { get; set; }
        public DateTime PublishDate { get; set; }
        public DateTime EditDate { get; set; }
        public int UserId { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public int StartTime { get; set; }
        public int EndTime { get; set; }
    }
}
