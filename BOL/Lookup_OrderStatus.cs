using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BOL
{
    [Table("Lookup_OrderStatus")]
   public partial class Lookup_OrderStatus
    {
        [Key]
        public int OrderStatusId { get; set; }
        public string OrderStatusName { get; set; }

    }
}
