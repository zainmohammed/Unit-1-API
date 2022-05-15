using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BOL
{
    [Table("Lookup_Country")]
   public partial  class Lookup_Country
    {
        [Key]
        public int Id { get; set; }
        public int CountryId { get; set; }
        public string CountryNameEng { get; set; }

    }
}
