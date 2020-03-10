using DealershipsManager.Models.Filters;
using DealershipsManager.Models.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DealershipsManager.Models.Dealership
{
    public class DealershipIndexViewModel
    {
        public PagerViewModel Pager { get; set; }

        public ICollection<DealershipDetailsViewModel> Items { get; set; }

        public FilterDealershipViewModel Filter { get; set; }
    }
}
