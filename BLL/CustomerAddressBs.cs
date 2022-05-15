using BOL;
using DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
   public class CustomerAddressBs
    {
        private CustomerAddressDb objDb;
        public CustomerAddressBs()
        {
            objDb = new CustomerAddressDb();
        }

        public bool InsertCustomerAddress(CustomerAddress cusAdd)
        {
            try
            {
                objDb.InsertCustomerAddress(cusAdd);
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
        public List<CustomerAddress> GetCustomerAddressById(int CustomerId)
        {
            return objDb.GetCustomerAddressById(CustomerId);
        }
        public bool UpdateCustomerAddress(CustomerAddress obj)
        {
            try
            {
                objDb.UpdateCustomerAddress(obj);
                return true;

            }
            catch (Exception ex)
            {

                return false;
            }
        }

    }
}
