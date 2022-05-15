using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BOL
{
    [Table("JobScreen")]
  public partial class Jobs
    {
        [Key]
        public int JobId { get; set; }
        public int CategoryId { get; set; }
        public string PointA { get; set; }
        public string PointALat { get; set; }
        public string PointALong { get; set; }
        public string PointB { get; set; }
        public string PointBLat { get; set; }
        public string PointBLong { get; set; }
        public int JobStatusId { get; set; }
        public double DeliveryCharges { get; set; }
        public DateTime DeliveryTime { get; set; }
        public double ItemCharges { get; set; }
        public DateTime DeliveredDate { get; set; }
        public DateTime PickedupDate { get; set; }
        public int DeliveryHeroRegistrationId { get; set; }
    }
}
