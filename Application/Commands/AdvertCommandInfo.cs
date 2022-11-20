using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Commands
{
    public class AdvertCommandInfo
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime DateIssue { get; set; }
        public string PTS { get; set; }
        public string VIN { get; set; }
        public string Description { get; set; }
        public DateTime PublishDate { get; set; }
        public DateTime EditDate { get; set; }
        public int Price { get; set; }
        public string UserName { get; set; }
        public List<ImageCommand> Images { get; set; }
        public List<AvailableTimeCommand> AvailableTimes { get; set; }
    }
}
