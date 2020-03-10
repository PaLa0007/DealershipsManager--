using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DealershipsManager.Data.Entities
{
    public class Dealership
    {
        public int DealershipId { get; set; }

        public string Name { get; set; }

        public string Country { get; set; }

        public string Town { get; set; }

        public virtual ICollection<Car> Cars { get; set; }

        public Dealership()
        {
            this.Cars = new HashSet<Car>();
        }
    }
}
