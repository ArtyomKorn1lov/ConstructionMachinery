using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Commands
{
    public class ImageCommand
    {
        public int Id { get; set; }
        public string Path { get; set; }
        public string RelativePath { get; set; }
        public int AdvertId { get; set; }
    }
}
