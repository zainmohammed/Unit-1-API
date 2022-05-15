using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BOL
{
    [Table("VendorRegistration")]
   public partial class VendorRegistration
    {
        [Key]
        public int VendorRegistrationId { get; set; }
        public int CategoryId { get; set; }
        public string OpenTime { get; set; }
        public string CloseTime { get; set; }
        public string PersonName { get; set; }
        public string WhatsappNo { get; set; }
        public string MobileNo { get; set; }
        public bool isContractSign { get; set; }
        public int UserId { get; set; }
        public int CreatedBy { get; set; }
    }
}
