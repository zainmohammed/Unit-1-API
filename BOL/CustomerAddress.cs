using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BOL
{
    [Table("CustomerAddress")]
    public partial class CustomerAddress
    {
        [Key]
        public int CustomerAddressId { get; set; }
        public int CustomerOrderId { get; set; }
        public string Lat { get; set; }
        public string Long { get; set; }
        public int CustomerId { get; set; }
        public string Address { get; set; }
        public string StreetName { get; set; }
        public string AreaName { get; set; }
        public string Zipcode { get; set; }
        public int AddressTypeId { get; set; }

       
    }
}
