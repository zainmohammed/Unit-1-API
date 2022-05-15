using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BOL
{
    [Table("Category")]
   public partial class Category
    {
        [Key]
        public int CategoryId { get; set; }
        public string CategoryNameEn { get; set; }
        public string CategoryNameAr { get; set; }


    }
}
