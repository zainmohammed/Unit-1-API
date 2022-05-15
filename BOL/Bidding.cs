using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BOL
{
    [Table("Bidding")]
   public partial class Bidding
    {
        [Key]
        public int BiddingId { get; set; }
        public DateTime DeliveryTime { get; set; }
        public double DeliveryCharges { get; set; }
        public string DeliveryExperience { get; set; }
        public bool IsSelect { get; set; }

    }
}
