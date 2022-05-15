using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BOL
{
    [Table("Customer")]
    public partial class Customer
    {
        [Key]
        public int CustomerId { get; set; }
        public string CustomerIdentificationNo { get; set; }
        public string EmailId { get; set; }
        public string MobileNo { get; set; }
        public string Password { get; set; }
        public string WhatsappNo { get; set; }
        public string Name { get; set; }
        public string DOB { get; set; }
        public int GenderId { get; set; }
        public int CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public int UpdatedBy { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public bool IsDelete { get; set; }
        public bool IsActive { get; set; }

        public string FromNumber { get; set; }
        public string ToNumber { get; set; }

    }
}
