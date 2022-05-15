using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BOL
{
    [Table("InvoiceDetails")]
   public partial  class InvoiceDetails
    {
        [Key]
        public int InvoiceDetailId { get; set; }
        public int InvoiceId { get; set; }
        public int CustomerId { get; set; }
        public double DiscountPrice { get; set; }
        public double ExcessPrice { get; set; }
        public double CostPrice { get; set; }
        public double SellingPrice { get; set; }
        public double Tax { get; set; }
        public double NetPrice { get; set; }
        public int CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public int UpdatedBy { get; set; }
        public DateTime UpdatedDate { get; set; }
        public bool IsDelete { get; set; }
    }
}
