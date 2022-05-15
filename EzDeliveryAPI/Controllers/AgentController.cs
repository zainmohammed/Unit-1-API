using BLL;
using BOL;
using EzDeliveryAPI.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;

namespace EzDeliveryAPI.Controllers
{
    public class AgentController : ApiController
    {
        CustomerBs customerBs;
        Common objCommon;
        public AgentController()
        {
            customerBs = new CustomerBs();
            objCommon = new Common();
        }
        [HttpPost]
        public ResponseObj<AgentRegistration> CreateAgent(AgentModel obj)
        {
            ResponseObj<AgentRegistration> returnObj = new ResponseObj<AgentRegistration>();
            AgentRegistration agent = new AgentRegistration();
            User user = new User();
            try
            {
                agent.AgentName = obj.AgentName;
                agent.DOB = obj.DOB;
                agent.HireDate = obj.HireDate;
                agent.JoinDate = obj.JoinDate;
                agent.Location = obj.Location;
                agent.MobileNo = obj.MobileNo;
                agent.WhatsappNo = obj.WhatsappNo;
                string httpPath = HttpContext.Current.Request.Url.AbsoluteUri.Replace("api", "|").Split('|')[0];
                Common common = new Common();
                if (obj.AdharCard != null)
                {
                    var p = common.SaveImage(obj.AdharCard, "Employee", Convert.ToString(agent.AgentRegistrationId), "Image");
                    agent.AdharCard = httpPath + p;
                }
                if (customerBs.InsertAgent(agent))
                {
                    returnObj.isSuccess = true;
                    user.UserName = obj.UserName;
                    user.Password = obj.Password;
                    user.RoleId = 2;
                    user.IsActive = true;
                    if (customerBs.InsertUser(user))
                    {
                        var agentInfo = customerBs.GetAgentById(agent.AgentRegistrationId);
                        agentInfo.UserId = user.UserId;
                        customerBs.UpdateAgent(agentInfo);
                        returnObj.Message = "Agent details Inserted Successfully";
                        returnObj.Data = agent;
                    }
                }
                else
                {
                    returnObj.isSuccess = false;
                    returnObj.Message = "Agent details Not Inserted";
                }
            }
            catch (Exception ex)
            {
                Common common = new Common();
                common.ExeceptionHandleWithMethodName(ex, "Agent,  CreateAgent");
                returnObj.Message = ex.Message;
                returnObj.isSuccess = false;
                return returnObj;
            }
            return returnObj;
        }
        [HttpPost]
        public ResponseObj<DeliveryHeroRegistration> CreateDeliveryHero(DeliveryHeroModel obj)
        {
            ResponseObj<DeliveryHeroRegistration> returnObj = new ResponseObj<DeliveryHeroRegistration>();
            DeliveryHeroRegistration hero = new DeliveryHeroRegistration();
            User user = new User();
            try
            {
                hero.AgentRegistrationId = obj.AgentRegistrationId;
                hero.AvailabilitySlot = obj.AvailabilitySlot;
                hero.DOB = obj.DOB;
                hero.HeroName = obj.HeroName;
                hero.MobileNo = obj.MobileNo;
                hero.WhatsappNo = obj.WhatsappNo;
                hero.Location = obj.Location;
                string httpPath = HttpContext.Current.Request.Url.AbsoluteUri.Replace("api", "|").Split('|')[0];
                Common common = new Common();
                if (obj.AdharCard != null)
                {
                    var p = common.SaveImage(obj.AdharCard, "Employee", Convert.ToString(hero.DeliveryHeroRegistrationId), "Image");
                    hero.AdharCard = httpPath + p;
                }
                if (obj.DrivingLicense != null)
                {
                    var p = common.SaveImage(obj.DrivingLicense, "Employee", Convert.ToString(hero.DeliveryHeroRegistrationId), "Image");
                    hero.DrivingLicense = httpPath + p;
                }
                if (customerBs.InsertDeliveryHero(hero))
                {
                    returnObj.isSuccess = true;
                    returnObj.Message = "Deliery Hero Inserted";
                    user.UserName = obj.UserName;
                    //user.Password = obj.Password;
                    user.RoleId = 3;
                    if (customerBs.InsertUser(user))
                    {
                        var HeroInfo = customerBs.GetHeroById(hero.DeliveryHeroRegistrationId);
                        HeroInfo.UserId = user.UserId;
                        customerBs.UpdateHero(HeroInfo);
                        returnObj.Message = "Delivery Hero details Inserted Successfully";
                        returnObj.Data = hero;
                    }
                }
                else
                {
                    returnObj.isSuccess = false;
                    returnObj.Message = "Delivery Hero Not Inserted";

                }
            }
            catch (Exception ex)
            {
                Common common = new Common();
                common.ExeceptionHandleWithMethodName(ex, "Agent,  CreateDeliveryHero");
                returnObj.Message = ex.Message;
                returnObj.isSuccess = false;
                return returnObj;
            }
            return returnObj;
        }

        [HttpPost]
        public ResponseObj<VendorRegistration> CreateVendor(VendorModel obj)
        {
            ResponseObj<VendorRegistration> returnObj = new ResponseObj<VendorRegistration>();
            VendorRegistration vendor = new VendorRegistration();
            User user = new User();
            try
            {
                vendor.CategoryId = obj.CategoryId;
                vendor.PersonName = obj.PersonName;
                vendor.MobileNo = obj.MobileNo;
                vendor.OpenTime = obj.OpenTime;
                vendor.CloseTime = obj.CloseTime;
                vendor.WhatsappNo = obj.WhatsappNo;
                vendor.isContractSign = true;
                if (customerBs.InsertVendor(vendor))
                {
                    returnObj.isSuccess = true;
                    user.UserName = obj.UserName;
                    user.Password = obj.Password;
                    user.RoleId = 5;
                    user.IsActive = true;
                    if (customerBs.InsertUser(user))
                    {
                        var vendorInfo = customerBs.GetVendorById(vendor.VendorRegistrationId);
                        vendorInfo.UserId = user.UserId;
                        customerBs.UpdateVendor(vendorInfo);
                        returnObj.isSuccess = true;
                        returnObj.Message = "Vendor Inserted Successfully";
                        returnObj.Data = vendor;
                    }
                }
                else
                {
                    returnObj.isSuccess = false;
                    returnObj.Message = "Vendor Not Inserted";
                }
            }
            catch (Exception ex)
            {
                Common common = new Common();
                common.ExeceptionHandleWithMethodName(ex, "Agent,  CreateVendor");
                returnObj.Message = ex.Message;
                returnObj.isSuccess = false;
                return returnObj;
            }
            return returnObj;
        }

        //[HttpPost]
        //public ResponseObj<Customer> CreateCustomer(CustomerModel obj)
        //{
        //    ResponseObj<Customer> returnObj = new ResponseObj<Customer>();
        //    Customer cus = new Customer();
        //    User user = new User();
        //    try
        //    {

        //        var MobileVerify = customerBs.GetMaxOTPByMobileNo(obj.MobileNo);
        //        int User = customerBs.GetAll().Where(x => x.UserName == obj.UserName).ToList().Count();
        //        int Mobile = customerBs.GetAllCustomer().Where(x => x.MobileNo == obj.MobileNo).ToList().Count();

        //        if (MobileVerify == null)
        //        {
        //            returnObj.Message = "OTP Does Not Found On This " + obj.MobileNo + "  No";
        //            returnObj.isSuccess = false;
        //            return returnObj;
        //        }

        //        if (MobileVerify.OTP != obj.OTP)
        //        {
        //            returnObj.Message = "OTP Does Not Match";
        //            returnObj.isSuccess = false;
        //            return returnObj;
        //        }

        //        if (User != 0 && Mobile == 0)
        //        {
        //            returnObj.Message = "UserName Already Exist";
        //            returnObj.isSuccess = false;
        //            return returnObj;
        //        }
        //        else if (Mobile != 0 && User == 0)
        //        {
        //            returnObj.Message = "MobileNo Already Exist";
        //            returnObj.isSuccess = false;
        //            return returnObj;
        //        }
        //        else if (User != 0 && Mobile != 0)
        //        {
        //            returnObj.Message = "MobileNo and Username Already Exist";
        //            returnObj.isSuccess = false;
        //            return returnObj;
        //        }


        //        cus.Name = obj.Name;
        //        cus.MobileNo = obj.MobileNo;
        //        cus.WhatsappNo = obj.WhatsappNo;
               
        //        cus.DOB = obj.DOB;
        //        cus.EmailId = obj.EmailId;
        //        cus.GenderId = obj.GenderId;
               
        //        cus.CreatedDate = DateTime.Now;
        //        cus.IsDelete = false;
        //        cus.IsActive = true;
               
                
        //        if (customerBs.InsertCustomer(cus))
        //        {
        //            returnObj.isSuccess = true;
        //            user.UserName = obj.UserName;
        //            user.Password = obj.Password;
        //            user.RoleId = 4;
        //            user.IsActive = true;
        //            if (customerBs.InsertUser(user))
        //            {
        //                var custInfo = customerBs.GetCustomerByCustomerId(cus.CustomerId);
        //                customerBs.UpdateCustomer(custInfo);
        //                returnObj.isSuccess = true;
        //                returnObj.Message = "Customer Inserted Successfully";
        //                returnObj.Data = cus;
        //            }
        //        }
        //        else
        //        {
        //            returnObj.isSuccess = false;
        //            returnObj.Message = "Customer Not Inserted";
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        Common common = new Common();
        //        common.ExeceptionHandleWithMethodName(ex, "Agent,  CreateCustomer");
        //        returnObj.Message = ex.Message;
        //        returnObj.isSuccess = false;
        //        return returnObj;
        //    }
        //    return returnObj;
        //}

        [HttpPost]
        public ResponseObj<Customer> UpdateCustomer(CustomerModel obj)
        {
            ResponseObj<Customer> returnObj = new ResponseObj<Customer>();
            try
            {
                var custInfo = customerBs.GetCustomerByCustomerId(obj.CustomerId);
                if (custInfo == null)
                {
                        returnObj.isSuccess = false;
                        returnObj.Message = "No Records Found";
                        return returnObj;
                }
                custInfo.Name = obj.Name;
                custInfo.MobileNo = obj.MobileNo;
                custInfo.WhatsappNo = obj.WhatsappNo;
               
                custInfo.DOB = obj.DOB;
                custInfo.EmailId = obj.EmailId;
                custInfo.GenderId = obj.GenderId;
                
                custInfo.UpdatedDate = DateTime.Now;
                custInfo.IsDelete = false;
                custInfo.IsActive = true;
               
                if (customerBs.UpdateCustomer(custInfo))
                {
                    returnObj.isSuccess = true;
                    returnObj.Message = "Customer Updated Successfully";
                    returnObj.Data = custInfo;
                }
                else
                {
                    returnObj.isSuccess = false;
                    returnObj.Message = "Customer Not Updated";
                }
            }
            catch (Exception ex)
            {
                Common common = new Common();
                common.ExeceptionHandleWithMethodName(ex, "Agent,  UpdateCustomer");
                returnObj.Message = ex.Message;
                returnObj.isSuccess = false;
                return returnObj;
            }
            return returnObj;
        }
        [HttpGet]
        public ResponseObj<Customer> GetCustomerByCustomerId(int custId)
        {
            ResponseObj<Customer> returnObj = new ResponseObj<Customer>();
            Customer cust = new Customer();
            try
            {
                cust= customerBs.GetCustomerByCustomerId(custId);
                if (cust != null)
                {
                    User userInfo = new User();
                    //userInfo = customerBs.GetUserById(cust.UserId);
                    //if (userInfo != null)
                    //{
                    //    cust.UserId = userInfo.UserId;
                    //    cust.UserName = userInfo.UserName;

                    //}
                    returnObj.isSuccess = true;
                    returnObj.Message = "Customer Detail By Customer Id";
                    returnObj.Data = cust;
                }
                else
                {
                    returnObj.isSuccess = false;
                    returnObj.Message = "Customer Not Found";
                }
            }
            catch (Exception ex)
            {
                Common common = new Common();
                common.ExeceptionHandleWithMethodName(ex, "Agent,  GetCustomerByCustomerId");
                returnObj.Message = ex.Message;
                returnObj.isSuccess = false;
                return returnObj;
            }
           return returnObj;
        }
        [HttpPost]
        public ResponseObj<OTPMobileVerification> VerifyMobileNo(string MobileNo)
        {
            //lblResult.Text = strrandom;
            // User userOj = new User();
            OTPMobileVerification obj = new OTPMobileVerification();
            ResponseObj<OTPMobileVerification> returnObj = new ResponseObj<OTPMobileVerification>();
            try
            {
                if (ModelState.IsValid)
                {
                    MobileNo = "91" + MobileNo;
                    int Mobile = customerBs.GetAllCustomer().Where(x => x.MobileNo == MobileNo).ToList().Count();

                    
                         if (Mobile != 0 )
                        {
                            returnObj.Message = "MobileNo Already Exist";
                            returnObj.isSuccess = false;
                            return returnObj;
                        }
                    

                    string mobileVerification = ConfigurationManager.AppSettings["MobileVerification"];
                    //  ConfigurationManager.Save(ConfigurationSaveMode.Modified);
                    string OTP = string.Empty;

                    if (mobileVerification != "Test")
                    {
                        OTP = objCommon.GenerateRandonPassword("0123456789", 4);

                        string OTPMsg = "Activation code: " + OTP + " to complete your registration";

                      SendSMSViaPlivo.SendSMSviaAPI(MobileNo, OTPMsg);
                    }
                    else
                    {
                        OTP = "1111";
                    }
                    obj.OTPMobileNo = MobileNo;
                    obj.OTP = OTP;
                    obj.IsValid = true;
                    obj.OTPTime = DateTime.Now;

                    if (customerBs.InsertOTPMobileVerification(obj))
                    {
                        obj.OTP = null;
                        returnObj.isSuccess = true;
                        returnObj.Data = obj;
                        returnObj.Message = "Otp Sent Successfully";
                        
                    }
                    else
                    {
                        var Error = ModelState.Values.SelectMany(x => x.Errors).FirstOrDefault();
                        ModelState.Clear();
                        returnObj.isSuccess = false;
                        returnObj.Message = Error.ErrorMessage;
                    }
                }
            }
            catch(Exception ex)
            {
                Common common = new Common();
                common.ExeceptionHandleWithMethodName(ex, "Agent,  VerifyMobileNo");
                returnObj.Message = ex.Message;
                returnObj.isSuccess = false;
                return returnObj;
            }
            return returnObj;
        }

        [HttpGet]
        public ResponseObj<List<AgentRegistration>> GetAllAgents()
        {
            ResponseObj<List<AgentRegistration>> returnObj = new ResponseObj<List<AgentRegistration>>();
            List<AgentRegistration> listagents = new List<AgentRegistration>();
            try
            {
                listagents = customerBs.GetAllAgents();
                if (listagents.Count > 0)
                {
                    returnObj.isSuccess = true;
                    returnObj.Message = "List of Agents";
                    returnObj.Data = listagents.OrderByDescending(x => x.AgentRegistrationId).ToList();
                }
                else
                {
                    returnObj.isSuccess = false;
                    returnObj.Message = "List of Agents Not Found";
                }
            }
            catch (Exception ex)
            {
                Common common = new Common();
                common.ExeceptionHandleWithMethodName(ex, "Agent,  GetAllAgents");
                returnObj.Message = ex.Message;
                returnObj.isSuccess = false;
                return returnObj;
            }
            return returnObj;
        }
        [HttpGet]
        public ResponseObj<AgentRegistration> GetAgentById(int agentId)
        {
            ResponseObj<AgentRegistration> returnObj = new ResponseObj<AgentRegistration>();
            AgentRegistration agent = new AgentRegistration();
            try
            {
                agent = customerBs.GetAgentById(agentId);
                if (agent != null)
                {
                    returnObj.isSuccess = true;
                    returnObj.Message = "Get Agent By Id";
                    returnObj.Data = agent;
                }
                else
                {
                    returnObj.isSuccess = false;
                    returnObj.Message = "Agent Not Found";
                }

            }
            catch (Exception ex)
            {
                Common common = new Common();
                common.ExeceptionHandleWithMethodName(ex, "Agent,  GetAgentById");
                returnObj.Message = ex.Message;
                returnObj.isSuccess = false;
                return returnObj;
            }
            return returnObj;
        }
        [HttpPost]
        public ResponseObj<Customer> CreateCustomer(CustomerRegisterModel obj)
        {
            ResponseObj<Customer> returnObj = new ResponseObj<Customer>();
            Customer cus = new Customer();

            try
            {
                obj.MobileNo = "91" + obj.MobileNo;
                var MobileVerify = customerBs.GetMaxOTPByMobileNo(obj.MobileNo);
                int Mobile = customerBs.GetAllCustomer().Where(x => x.MobileNo == obj.MobileNo).ToList().Count();

                if (MobileVerify == null)
                {
                    returnObj.Message = "OTP Does Not Found On This " + obj.MobileNo + "  No";
                    returnObj.isSuccess = false;
                    return returnObj;
                }

                if (MobileVerify.OTP != obj.Otp)
                {
                    returnObj.Message = "OTP Does Not Match";
                    returnObj.isSuccess = false;
                    return returnObj;
                }

                cus.Password = obj.Password;
                cus.MobileNo = obj.MobileNo;
                cus.CreatedDate = DateTime.Now;
                cus.IsDelete = false;
                cus.IsActive = true;

                if (customerBs.InsertCustomer(cus))
                {
                    returnObj.Data = cus;
                    returnObj.isSuccess = true;
                    returnObj.Message = "Customer Created Sucessfully";
                }
                else
                {
                    returnObj.isSuccess = false;
                    returnObj.Message = "Customer Created UnSucessfully";
                }
                }
            catch (Exception ex)
            {
                Common common = new Common();
                common.ExeceptionHandleWithMethodName(ex, "Agent,  CreateCustomer");
                returnObj.Message = ex.Message;
                returnObj.isSuccess = false;
                return returnObj;
            }
            return returnObj;
        }

        [HttpPost]
        public ResponseObj<Customer> UpdateCustomer(UpdateCustomerModel customerObj)
        {
            ResponseObj<Customer> returnObj = new ResponseObj<Customer>();
            Customer customer = new Customer();

            customer = customerBs.GetCustomerByCustomerId(customerObj.CustomerId);

            customer.DOB = customerObj.DOB;
            customer.EmailId = customerObj.EmailId;
            customer.GenderId = customerObj.GenderId;
            customer.MobileNo = customerObj.MobileNo;
            customer.Name = customerObj.Name;
            customer.Password = customerObj.Password;
            customer.UpdatedBy = customerObj.CustomerId;
            customer.UpdatedDate = DateTime.Now;
            customer.WhatsappNo = customerObj.WhatsappNo;

            if (customerBs.UpdateCustomer(customer))
            {
                returnObj.Data = customer;
                returnObj.isSuccess = true;
                returnObj.Message = "Customer Updated Successfully";
            }
            else
            {
                returnObj.isSuccess = false;
                returnObj.Message = "Customer not Updated";
            }
            return returnObj;
        }

        
    }
}