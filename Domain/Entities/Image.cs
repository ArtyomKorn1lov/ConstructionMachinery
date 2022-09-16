using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Image
    {
        public int Id { get; set; }
        public string Path { get; set; }
        public string RelativePath { get; set; }
        public int AdvertId { get; set; }

        public void CopyFrom(Image image)
        {
            this.Id = image.Id;
            this.Path = image.Path;
            this.RelativePath = image.RelativePath;
            this.AdvertId = image.AdvertId;
        }
    }
}
