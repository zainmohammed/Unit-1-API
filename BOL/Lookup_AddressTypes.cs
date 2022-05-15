using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BOL
{
    [Table("Lookup_AddressTypes")]
   public class Lookup_AddressTypes
    {
        [Key]
        public int AddressTypeId { get; set; }
        public string AddressName { get; set; }
    }
}
