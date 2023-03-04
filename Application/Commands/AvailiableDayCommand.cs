using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Commands
{
    public class AvailiableDayCommand
    {
        public DateTime Date { get; set; }
        public List<AvailableTimeCommand> Times { get; set; }
    }
}
