using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BOL
{
    [Table("OrderStatusLog")]
   public partial class OrderStatusLog
    {
        [Key]
        public int OrderStatusLogId { get; set; }
        public int OrderStatusId { get; set; }
        public int CustomerOrderId { get; set; }
        //public int OrderStatusId { get; set; }
        public DateTime OrderStatusDateTime { get; set; }
    }
}
