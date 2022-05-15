using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
   public class ModelDb
    {
        public class CustomerOrderDetailsModel
        {
            public string FromNumber { get; set; }
            public string ToNumber { get; set; }
            public int CustomerOrderId { get; set; }
            public string CustomerOrderNo { get; set; }
            public string OrderDetailText { get; set; }
            public string ProductPic { get; set; }
            public int CategoryId { get; set; }
            public string CategoryNameEn { get; set; }
            public DateTime? OrderDate { get; set; }
            public DateTime? DeliveryStartTime { get; set; }
            public DateTime? DeliveryEndTime { get; set; }
            public int OrderDriverId { get; set; }
            public string HeroName { get; set; }
            public string DriverMobileNumber { get; set; }
            public int OrderStatusId { get; set; }
            public  string OrderStatusName { get; set; }
            public decimal ValueOfGood { get; set; }
            public decimal Weight { get; set; }
            public string Size { get; set; }
            public int PickupPoint { get; set; }
            public string PickUpAddress { get; set; }
            public string LatP { get; set; }
            public string LongP { get; set; }
            public string ZipcodeP { get; set; }
            public int DeliveryPoint { get; set; }
            public string DeliveryAddress { get; set; }
            public string LatD { get; set; }
            public string LongD { get; set; }
            public string ZipcodeD { get; set; }
            public string Name { get; set; }
            public decimal BidPrice { get; set; }
        }
    }
}
