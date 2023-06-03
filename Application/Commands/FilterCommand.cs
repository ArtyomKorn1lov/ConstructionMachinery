using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Commands
{
    public class FilterCommand
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
