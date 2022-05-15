using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BOL
{
    [Table("CustomerOrder")]
    public partial class CustomerOrder
    {
        [Key]
        public int CustomerOrderId { get; set; }
        public string CustomerOrderNo { get; set; }
        public string FromNumber { get; set; }
        public string ToNumber { get; set; }
        public int CustomerId { get; set; }
        public int CustomerAddressId { get; set; }
        public int OrderStatusId { get; set; }
        public string OrderDetailText { get; set; }
        public int? PickupPoint { get; set; }
        public int? DeliveryPoint { get; set; }
        public string OrderDetailVoice { get; set; }
        public string ProductPic { get; set; }
        public int CategoryId { get; set; }
        public int VendorRegistrationId { get; set; }
        public int OrderDriverId { get; set; }
        public decimal ValueOfGood { get; set; }
        public decimal Weight { get; set; }
        public string Size { get; set; }
        public DateTime? Date { get; set; }
        public DateTime? DeliveryStartTime { get; set; }
        public DateTime? DeliveryEndTime { get; set; }
        public bool IsOrderFlag { get; set; }
       

       
    }
}
