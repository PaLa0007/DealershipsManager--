using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DealershipsManager.Models.Shared
{
    public class PagerViewModel
    {
        public int CurrentPage { get; set; }

        public int PagesCount { get; set; }

        public int Page { get; set; }

        public int ItemsPerPage { get; set; }
    }
}
