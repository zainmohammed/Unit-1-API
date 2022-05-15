using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BOL
{
    [Table("Lookup_City")]
  public partial class Lookup_City
    {
        [Key]
        public int Id { get; set; }
        public int CityId { get; set; }
        public string CityNameEng { get; set; }
        public int CountryId { get; set; }
    }
}
