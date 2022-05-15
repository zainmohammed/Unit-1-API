using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BOL
{
    [Table("Invoice")]
   public partial  class Invoice
    {
        [Key]
        public int InvoiceId { get; set; }
        public string InvoiceIdentificationNo { get; set; }
        public DateTime InvoiceDate { get; set; }
        public int CustomerId { get; set; }
        public double TotalDiscount { get; set; }
        public double TotalAmount { get; set; }
        public double TotalTax { get; set; }
        public double GrandTotalAmount { get; set; }
        public int InvoiceStatusId { get; set; }
        public int Channel { get; set; }
        public int CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public int UpdatedBy { get; set; }
        public DateTime UpdatedDate { get; set; }
        public bool IsDelete { get; set; }
    }
}
