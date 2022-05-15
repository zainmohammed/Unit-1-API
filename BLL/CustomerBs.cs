using BOL;
using DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
  public  class CustomerBs
    {
        private CustomerDb objDb;
        public CustomerBs()
        {
            objDb = new CustomerDb();
        }
        public bool InsertCustomer(Customer cus)
        {
            try
            {
                objDb.InsertCustomer(cus);
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
        public bool InsertAgent(AgentRegistration agent)
        {
            try
            {
                objDb.InsertAgent(agent);
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
        public bool InsertDeliveryHero(DeliveryHeroRegistration hero)
        {
            try
            {
                objDb.InsertDeliveryHero(hero);
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
        public bool InsertUser(User user)
        {
            try
            {
                objDb.InsertUser(user);
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
        public bool InsertVendor(VendorRegistration vendor)
        {
            try
            {
                objDb.InsertVendor(vendor);
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
        public bool UpdateCustomer(Customer cus)
        {
            try
            {
                objDb.UpdateCustomer(cus);
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
        public bool UpdateAgent(AgentRegistration agent)
        {
            try
            {
                objDb.UpdateAgent(agent);
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
        public bool UpdateHero(DeliveryHeroRegistration hero)
        {
            try
            {
                objDb.UpdateHero(hero);
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
        public bool UpdateVendor(VendorRegistration vendor)
        {
            try
            {
                objDb.UpdateVendor(vendor);
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
        public AgentRegistration GetAgentById(int agtId)
        {
            return objDb.GetAgentById(agtId);
        }
        public AgentRegistration GetAgentByUserId(int userId)
        {
            return objDb.GetAgentByUserId(userId);
        }
        public DeliveryHeroRegistration GetHeroByUserId(int userId, string MobileNo, string Password)
        {
            return objDb.GetHeroByUserId(userId);
        }
        //public Customer GetCustomerByUserId(int userId)
        //{
        //    return objDb.GetCustomerByUserId(userId);
        //}

        public VendorRegistration GetVendorByUserId(int userId)
        {
            return objDb.GetVendorByUserId(userId);
        }
        public DeliveryHeroRegistration GetHeroById(int heroId)
        {
            return objDb.GetHeroById(heroId);
        }
        public VendorRegistration GetVendorById(int vendorId)
        {
            return objDb.GetVendorById(vendorId);
        }
        public Customer GetCustomerByCustomerId(int cusId)
        {
            return objDb.GetCustomerByCustomerId(cusId);
        }
        public List<User> GetAll()
        {
            return objDb.GetAll();
        }

        public Customer getCustomerByMobile(string MobileNo)
        {
            return objDb.getCustomerByMobile(MobileNo);
        }
        public Customer getCustomerByMobileandPassword(string MobileNo)
        {
            return objDb.getCustomerByMobileandPassword(MobileNo);
        }
            public List<Customer> GetAllCustomer()
        {
            return objDb.GetAllCustomer();
        }
        public bool InsertOTPMobileVerification(OTPMobileVerification otpverfication)
        {
            try
            {
                objDb.InsertOTPMobileVerification(otpverfication);
                return true;
            }
            catch (Exception ex)
            {

                return false;
            }
        }

        public OTPMobileVerification GetMaxOTPByMobileNo(string MobileNo)
        {
            return objDb.GetMaxOTPByMobileNo(MobileNo);
        }

        public List<AgentRegistration> GetAllAgents()
        {
            return objDb.GetAllAgents();
        }
        public User GetUserById(int userId)
        {
            return objDb.GetUserById(userId);
        }

        public List<Customer> GetAllCustomerList()
        {
            return objDb.GetAllCustomerList();
        }
       
    }
}
