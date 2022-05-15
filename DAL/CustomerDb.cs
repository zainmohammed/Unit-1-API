using BOL;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
   public  class CustomerDb
    {
        private Context db;
        public CustomerDb()
        {
            db = new Context();
        }
        public void InsertCustomer(Customer cus)
        {
            db.Customer.Add(cus);
            Save();
        }
        public void InsertAgent(AgentRegistration agent)
        {
            db.AgentRegistration.Add(agent);
            Save();
        }
        public void InsertDeliveryHero(DeliveryHeroRegistration hero)
        {
            db.DeliveryHeroRegistration.Add(hero);
            Save();
        }
        public void InsertUser(User user)
        {
            db.User.Add(user);
            Save();
        }
        public void InsertVendor(VendorRegistration vendor)
        {
            db.VendorRegistration.Add(vendor);
            Save();
        }
        public void UpdateCustomer(Customer cus)
        {
            db.Entry(cus).State = EntityState.Modified;
            db.Configuration.ValidateOnSaveEnabled = false;
            Save();
            db.Configuration.ValidateOnSaveEnabled = true;
        }
        public void UpdateAgent(AgentRegistration agent)
        {
            db.Entry(agent).State = EntityState.Modified;
            db.Configuration.ValidateOnSaveEnabled = false;
            Save();
            db.Configuration.ValidateOnSaveEnabled = true;
        }
        public void UpdateHero(DeliveryHeroRegistration hero)
        {
            db.Entry(hero).State = EntityState.Modified;
            db.Configuration.ValidateOnSaveEnabled = false;
            Save();
            db.Configuration.ValidateOnSaveEnabled = true;
        }
        public void UpdateVendor(VendorRegistration vendor)
        {
            db.Entry(vendor).State = EntityState.Modified;
            db.Configuration.ValidateOnSaveEnabled = false;
            Save();
            db.Configuration.ValidateOnSaveEnabled = true;
        }
        public AgentRegistration GetAgentById(int agtId)
        {
            return db.AgentRegistration.Where(x => x.AgentRegistrationId == agtId).FirstOrDefault();
        }
        public AgentRegistration GetAgentByUserId(int userId)
        {
            return db.AgentRegistration.Where(x => x.UserId == userId).FirstOrDefault();
        }
        public DeliveryHeroRegistration GetHeroByUserId(int userId)
        {
            return db.DeliveryHeroRegistration.Where(x => x.UserId == userId).FirstOrDefault();
        }
        //public Customer GetCustomerByUserId(int userId)
        //{
        //    return db.Customer.Where(x => x.UserId == userId).FirstOrDefault();
        //}

        public VendorRegistration GetVendorByUserId(int userId)
        {
            return db.VendorRegistration.Where(x => x.UserId == userId).FirstOrDefault();
        }
        public DeliveryHeroRegistration GetHeroById(int heroId)
        {
            return db.DeliveryHeroRegistration.Where(x => x.DeliveryHeroRegistrationId == heroId).FirstOrDefault();
        }
        public VendorRegistration GetVendorById(int vendorId)
        {
            return db.VendorRegistration.Where(x => x.VendorRegistrationId == vendorId).FirstOrDefault();
        }
        public Customer GetCustomerByCustomerId(int cusId)
        {
            return db.Customer.Where(x => x.IsActive == true && x.IsDelete == false && x.CustomerId == cusId).FirstOrDefault();
        }

        public Customer getCustomerByMobile(string MobileNo)
        {
            return db.Customer.Where(x => x.MobileNo == MobileNo).FirstOrDefault();
        }
        public Customer getCustomerByMobileandPassword(string MobileNo)
        {
            return db.Customer.Where(x => x.MobileNo == MobileNo).FirstOrDefault();
        }

        public List<User> GetAll()
        {
            return db.User.Where(x => x.IsActive == true).ToList();
        }
        public List<Customer> GetAllCustomer()
        {
            return db.Customer.Where(x => x.IsActive == true).ToList();
        }
        public void InsertOTPMobileVerification(OTPMobileVerification otpverfication)
        {
            db.OTPMobileVerification.Add(otpverfication);
            Save();
        }
        public OTPMobileVerification GetMaxOTPByMobileNo(string MobileNo)
        {
            {

                var OTP = db.OTPMobileVerification.Where(x => x.OTPMobileNo == MobileNo).ToList();

                if (OTP.Count == 0)
                {
                    return null;
                }
                return OTP.Last();
            }
        }
        public List<AgentRegistration> GetAllAgents()
        {
            return db.AgentRegistration.ToList();
        }
        public User GetUserById(int userId)
        {
            return db.User.Where(x =>x.IsActive==true && x.UserId == userId).FirstOrDefault();
        }
        public Role GetRoleByUser(int Id)
        {
            return db.Role.Where(x => x.RoleId == Id).FirstOrDefault();
        }
        private void Save()
        {
            db.SaveChanges();
        }

        public List<Customer> GetAllCustomerList()
        {
            return db.Customer.ToList();
        }



    }
}
