using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Commands
{
    public class FilterCommand
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
