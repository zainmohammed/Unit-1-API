using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BOL
{
    [Table("AgentRegistration")]
   public partial class AgentRegistration
    {
        [Key]
        public int AgentRegistrationId { get; set; }
        public string AgentName { get; set; }
        public string MobileNo { get; set; }
        public string WhatsappNo { get; set; }
        public string AdharCard { get; set; }
        public string DOB { get; set; }
        public string Location { get; set; }
        public string  HireDate { get; set; }
        public string  JoinDate { get; set; }
        public int UserId { get; set; }

    }
}
