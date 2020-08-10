using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using veilingservice.Model;

namespace veilingservice.Data
{
    public class VeilingContext : DbContext
    {
        public VeilingContext(DbContextOptions<VeilingContext> options)
            : base(options)
        {

        }

        public DbSet<Auction> Auction { get; set; }
        public DbSet<AuctionImage> AuctionImage { get; set; }

        public DbSet<Lot> Lot { get; set; }
        public DbSet<LotImage> LotImage { get; set; }

        public DbSet<User> Users { get; set; }

        public DbSet<Role> Role { get; set; }

        public DbSet<ApiKey> ApiKey { get; set; }
    }
}
