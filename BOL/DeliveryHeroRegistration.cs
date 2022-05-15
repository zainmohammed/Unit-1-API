using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BOL
{
    [Table("DeliveryHeroRegistration")]
   public partial class DeliveryHeroRegistration
    {
        [Key]
        public int DeliveryHeroRegistrationId { get; set; }
        public string HeroName { get; set; }
        public string MobileNo { get; set; }
        public string WhatsappNo { get; set; }
        public string AdharCard { get; set; }
        public string DrivingLicense { get; set; }
        public string AvailabilitySlot { get; set; }
        public string DOB { get; set; }
        public string Location { get; set; }
        public int AgentRegistrationId { get; set; }
        public int UserId { get; set; }
        public string PlateNo { get; set; }
        public int CreatedBy { get; set; }

        public bool IsActive { get; set; }

        //public string Password { get; set; }

    }
}
