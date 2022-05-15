using BOL;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
   public class CustomerAddressDb
    {
        private Context db;

        public CustomerAddressDb()
        {
            db = new Context();
        }

        public void InsertCustomerAddress(CustomerAddress cusAdd)
        {
            db.CustomerAddress.Add(cusAdd);
            Save();
        }
        public List<CustomerAddress> GetCustomerAddressById(int CustomerOrderId)
        {
            return db.CustomerAddress.Where(x => x.CustomerOrderId == CustomerOrderId).ToList();
        }
        public void UpdateCustomerAddress(CustomerAddress obj)
        {
            db.Entry(obj).State = EntityState.Modified;
            db.Configuration.ValidateOnSaveEnabled = false;
            Save();
            db.Configuration.ValidateOnSaveEnabled = true;
        }
        //public List<CustomerAddress> GetCustomerAddressList()
        //{

        //}
        private void Save()
        {
            db.SaveChanges();
        }
    }
}
