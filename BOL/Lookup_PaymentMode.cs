using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BOL
{
    [Table("Lookup_PaymentMode")]
   public partial class Lookup_PaymentMode
    {
        [Key]
        public int Id { get; set; }
        public int PaymentModeId { get; set; }
        public string PaymentModeEng { get; set; }
        public string PaymentModeAra { get; set; }
        public string Charges { get; set; }
    }
}
