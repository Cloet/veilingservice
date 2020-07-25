using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using veilingservice.Helpers;

namespace veilingservice.Model
{
    public class Auction
    {

        public int ID { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }

        public IEnumerable<Lot> Lots { get; set; }

        public IEnumerable<AuctionImage> Images { get; set; }

        public AuctionStatus Status { get; set; }

    }
}
