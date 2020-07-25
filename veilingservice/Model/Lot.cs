﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace veilingservice.Model
{
    public class Lot
    {
        [Required]
        public int ID { get; set; }

        [Required]
        public int AuctionID { get; set; }

        [Required]
        public int Number { get; set; }

        [StringLength(60,MinimumLength = 3)]
        [Required]
        public string Title { get; set; }

        public DateTime CurrentTime { get; set; }

        public DateTime StartTime { get; set; }

        public DateTime EndTime { get; set; }

        [StringLength(1000)]
        public string Description { get; set; }

        [DataType(DataType.Currency)]
        public double OpeningsBid { get; set; }

        [DataType(DataType.Currency)]
        public double CurrentBid { get; set; }

        public int AmountOfBids { get; set; }

        [DataType(DataType.Currency)]
        public double Bid { get; set; }


    }
}
