using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Commands
{
    public class AvailabilityRequestListCommand
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime Date { get; set; }
        public List<ImageCommand> Images { get; set; }
    }
}
