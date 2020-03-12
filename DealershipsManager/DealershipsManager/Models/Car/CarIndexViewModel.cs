using DealershipsManager.Models.Filters;
using DealershipsManager.Models.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DealershipsManager.Models.Car
{
    public class CarIndexViewModel
    {
        public PagerViewModel Pager { get; set; }

        public ICollection<CarViewModel> Items { get; set; }

        public FilterCarViewModel Filter { get; set; }
    }
}
