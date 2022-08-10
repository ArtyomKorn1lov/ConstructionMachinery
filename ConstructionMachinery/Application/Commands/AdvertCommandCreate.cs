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
        public int Price { get; set; }
        public int UserId { get; set; }
        public List<AvailableTimeCommandCreate> AvailableTimeCommandCreates { get; set; }
    }
}
