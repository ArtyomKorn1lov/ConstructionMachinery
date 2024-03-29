﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Commands
{
    public class AdvertCommandList
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Price { get; set; }
        public double AverageRating { get; set; }
        public DateTime EditDate { get; set; }
        public List<ImageCommand> Images { get; set; }
    }
}
