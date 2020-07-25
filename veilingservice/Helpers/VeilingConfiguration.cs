using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace veilingservice.Helpers
{
    public static class VeilingConfiguration
    {

        public static string BaseFileLocation { get; set; }

        public static string ImageLocation => BaseFileLocation + $"{Path.DirectorySeparatorChar}images";

        public static string AuctionImageLocation => ImageLocation + $"{Path.DirectorySeparatorChar}Auction";

        public static string LotImageLocation => ImageLocation + $"{Path.DirectorySeparatorChar}Lot";

    }
}
