using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BOL
{
    [Table("OrderInVoice")]
   public partial class OrderInVoice
    {
        [Key]
        public int OrderInVoiceId { get; set; }
        public byte[] VoiceOrder { get; set; }
    }
}
