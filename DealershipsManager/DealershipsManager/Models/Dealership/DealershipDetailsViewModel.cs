﻿using DealershipsManager.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DealershipsManager.Models.Dealership
{
    public class DealershipDetailsViewModel
    {
        public int DealershipId { get; set; }

        public string Name { get; set; }

        public string Country { get; set; }

        public string Town { get; set; }

        public virtual ICollection<Data.Entities.Car> Cars { get; set; }
    }
}
