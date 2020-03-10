using DealershipsManager.Models.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DealershipsManager.Models.User
{
    public class UserIndexViewModel
    {
        public PagerViewModel Pager { get; set; }

        public ICollection<DetailsUserViewModel> Items { get; set; }

        public FilterUserViewModel Filter { get; set; }
    }
}
