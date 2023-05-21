using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Commands
{
    public class LeaseRequestCommand
    {
        public int Price { get; set; }
        public List<AvailiableDayCommand> AvailiableDayCommands { get; set; }
    }
}
