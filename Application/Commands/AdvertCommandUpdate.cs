using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Commands
{
    public class AdvertCommandUpdate
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime DateIssue { get; set; }
        public string PTS { get; set; }
        public string VIN { get; set; }
        public string Description { get; set; }
        public int Price { get; set; }
        public DateTime EditDate { get; set; }
        public int UserId { get; set; }
        public List<ImageCommand> ImageCommands { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public int StartTime { get; set; }
        public int EndTime { get; set; }
    }
}
