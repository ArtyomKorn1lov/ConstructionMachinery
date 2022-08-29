﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Commands
{
    public class AvailabilityRequestCommandForCustomer
    {
        public int Id { get; set; }
        public string AvertName { get; set; }
        public string Address { get; set; }
        public int RequestStateId { get; set; }
        public string LandlordName { get; set; }
        public int UserId { get; set; }
        public string Phone { get; set; }
        public List<AvailableTimeCommand> AvailableTimeCommands { get; set; }

        public static implicit operator AvailabilityRequestCommandForCustomer(List<AvailabilityRequestCommandForCustomer> v)
        {
            throw new NotImplementedException();
        }
    }
}
