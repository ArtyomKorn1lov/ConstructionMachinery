using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Commands
{
    public class ReviewCommandCreate
    {
        public string Description { get; set; }
        public DateTime Date { get; set; }
        public int ReviewStateId { get; set; }
        public int AdvertId { get; set; }
        public int UserId { get; set; }
    }
}
