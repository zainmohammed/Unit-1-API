using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BOL
{
    [Table("OrderDriver")]
    public partial class OrderDriver
    {
        [Key]
        public int OrderDriverId { get; set; }
        public int CustomerOrderId { get; set; }
        public int DriverId { get; set; }
        public decimal BidPrice { get; set; }
        public bool IsSelected { get; set; }
        public string Time { get; set; }
        public string DeliveryNotes { get; set; }
        public int Status { get; set; }
      

        [NotMapped]
        public CustomerOrder CustomerOrder { get; set; }

        [NotMapped]
        public DeliveryHeroRegistration HeroRegistration { get; set; }

    }
}
