using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BOL
{
    [Table("OTPMobileVerification")]
   public partial class OTPMobileVerification
    {
        [Key]
        public int OTPMobileVerificationId { get; set; }
        public string OTP { get; set; }
        public string OTPMobileNo { get; set; }
        public bool IsValid { get; set; }
        public DateTime OTPTime { get; set; }
    }
}
