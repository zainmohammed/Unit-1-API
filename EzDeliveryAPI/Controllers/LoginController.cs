using BLL;
using BOL;
using EzDeliveryAPI.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace EzDeliveryAPI.Controllers
{
    public class LoginController : ApiController
    {
        LoginBs loginBs;
        CustomerBs customerBs;
        Common objCommon;
        public LoginController()
        {
            loginBs = new LoginBs();
            customerBs = new CustomerBs();
            objCommon = new Common();
        }
        [HttpPost]
        public ResponseObj<AgentLogin> AgentLogin(string UserName, string Password)
        {
            ResponseObj<AgentLogin> returnObj = new ResponseObj<AgentLogin>();
            AgentLogin obj = new AgentLogin();
            try
            {
                var UserInfo = loginBs.Login(UserName, Password);
                if (UserInfo != null)
                {
                    obj.UserId = UserInfo.UserId;
                    obj.UserName = UserInfo.UserName;
                    var RoleInfo = loginBs.GetRolesById(UserInfo.RoleId);
                    if (RoleInfo != null)
                    {
                        obj.RoleId = RoleInfo.RoleId;
                        obj.RoleName = RoleInfo.RoleName;
                    }
                    var agentInfo = customerBs.GetAgentByUserId(UserInfo.UserId);
                    if (agentInfo != null)
                    {
                        obj.AgentName=agentInfo.AgentName;
                        obj.AdharCard = agentInfo.AdharCard;
                        obj.DOB = agentInfo.DOB;
                        obj.HireDate = agentInfo.HireDate;
                        obj.JoinDate = agentInfo.JoinDate;
                        obj.Location = agentInfo.Location;
                        obj.MobileNo = agentInfo.MobileNo;
                        obj.WhatsappNo = agentInfo.WhatsappNo;
                    }
                    returnObj.Data = obj;
                    returnObj.isSuccess = true;
                    returnObj.Message = "Login Successfully";
                }
                else
                {
                    returnObj.isSuccess = false;
                    returnObj.Message = "Login UnSuccessfull";
                }
            }
            catch (Exception ex)
            {
                Common common = new Common();
                common.ExeceptionHandleWithMethodName(ex, "Login,  AgentLogin");
                returnObj.Message = ex.Message;
                returnObj.isSuccess = false;
                return returnObj;
            }
            return returnObj;
        }

        [HttpGet]
        public ResponseObj<AgentLogin> AdminLogin(string UserName, string Password)
        {
            ResponseObj<AgentLogin> returnObj = new ResponseObj<AgentLogin>();
            AgentLogin obj = new AgentLogin();
            try
            {
                var UserInfo = loginBs.Login(UserName, Password);
                if (UserInfo != null)
                {
                    obj.UserId = UserInfo.UserId;
                    obj.UserName = UserInfo.UserName;
                    var RoleInfo = loginBs.GetRolesById(UserInfo.RoleId);
                    if (RoleInfo != null)
                    {
                        if (RoleInfo.RoleId != 1)
                        {
                            returnObj.isSuccess = false;
                            returnObj.Message = "Admin Login";
                            return returnObj;
                        }

                        obj.RoleId = RoleInfo.RoleId;
                        obj.RoleName = RoleInfo.RoleName;
                    }
                }
            }
            catch (Exception ex)
            {
                Common common = new Common();
                common.ExeceptionHandleWithMethodName(ex, "Login,  Admin Login");
                returnObj.Message = ex.Message;
                returnObj.isSuccess = false;
                return returnObj;
            }
            return returnObj;
        }


        [HttpPost]
        public ResponseObj<HeroLogin> DeliveryHeroLogin(string MobileNo )
        {
            ResponseObj<HeroLogin> returnObj = new ResponseObj<HeroLogin>();
            HeroLogin obj = new HeroLogin();
            try
            {
                var HeroInfo = loginBs.HeroLogin(MobileNo);
                if (HeroInfo != null)
                {
                    obj.UserId = HeroInfo.DeliveryHeroRegistrationId;
                    obj.UserName = HeroInfo.HeroName;
                    obj.HeroName = HeroInfo.HeroName;
                    obj.AdharCard = HeroInfo.AdharCard;
                    obj.DOB = HeroInfo.DOB;
                    obj.DrivingLicense = HeroInfo.DrivingLicense;
                    obj.Location = HeroInfo.Location;
                    obj.MobileNo = HeroInfo.MobileNo;
                    obj.WhatsappNo = HeroInfo.WhatsappNo;
                    //obj.Password = HeroInfo.Password;
                    returnObj.Data = obj;
                    returnObj.isSuccess = true;
                    returnObj.Message = "Login Successfully";
                }
                else
                {
                    returnObj.isSuccess = false;
                    returnObj.Message = "Login UnSuccessfull";
                }
            }
            catch (Exception ex)
            {
                Common common = new Common();
                common.ExeceptionHandleWithMethodName(ex, "Login,  DeliveryHeroLogin");
                returnObj.Message = ex.Message;
                returnObj.isSuccess = false;
                return returnObj;
            }
            return returnObj;
        }

        //[HttpPost]
        //public ResponseObj<CustomerLogin> LoginCustomer(string UserName, string Password)
        //{
        //    ResponseObj<CustomerLogin> returnObj = new ResponseObj<CustomerLogin>();
        //    CustomerLogin obj = new CustomerLogin();

        //    try
        //    {
        //        var UserInfo = loginBs.Login(UserName, Password);
        //        if (UserInfo != null)
        //        {
        //            obj.UserId = UserInfo.UserId;
        //            obj.UserName = UserInfo.UserName;
        //            var RoleInfo = loginBs.GetRolesById(UserInfo.RoleId);
        //            if (RoleInfo != null)
        //            {
        //                obj.RoleId = RoleInfo.RoleId;
        //                obj.RoleName = RoleInfo.RoleName;
        //            }
        //            var customerInfo = customerBs.GetCustomerByUserId(UserInfo.UserId);
        //            if(customerInfo != null)
        //            {
                       
        //                obj.CustomerId = customerInfo.CustomerId;
        //                obj.DOB = customerInfo.DOB;
        //                obj.EmailId = customerInfo.EmailId;
        //                obj.GenderId = customerInfo.GenderId;
        //                obj.MobileNo = customerInfo.MobileNo;
        //                obj.Name = customerInfo.Name;
        //                //obj.UserId = customerInfo.UserId;
        //                //obj.UserName = customerInfo.UserName;
        //                obj.WhatsappNo = customerInfo.WhatsappNo;
                       
        //            }
        //            returnObj.Data = obj;
        //            returnObj.isSuccess = true;
        //            returnObj.Message = "Login Successfully";
        //        }
        //        else
        //        {
        //            returnObj.isSuccess = false;
        //            returnObj.Message = "Login UnSuccessfull";
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        Common common = new Common();
        //        common.ExeceptionHandleWithMethodName(ex, "Login,  CustomerLogin");
        //        returnObj.Message = ex.Message;
        //        returnObj.isSuccess = false;
        //        return returnObj;
        //    }
        //    return returnObj;
        //}

        [HttpPost]
        public ResponseObj<VendorLogin> LoginVendor(string UserName, string Password)
        {
            ResponseObj<VendorLogin> returnObj = new ResponseObj<VendorLogin>();
            VendorLogin obj = new VendorLogin();

            try
            {
                var UserInfo = loginBs.Login(UserName, Password);
                if (UserInfo != null)
                {
                    obj.UserId = UserInfo.UserId;
                    obj.UserName = UserInfo.UserName;
                    var RoleInfo = loginBs.GetRolesById(UserInfo.RoleId);
                    if (RoleInfo != null)
                    {
                        if(RoleInfo.RoleId!=5)
                        {
                            returnObj.isSuccess = false;
                            returnObj.Message = "Login Vendor";
                            return returnObj;
                        }
                        obj.RoleId = RoleInfo.RoleId;
                        obj.RoleName = RoleInfo.RoleName;
                    }
                    var vendorInfo = customerBs.GetVendorByUserId(UserInfo.UserId);
                    if(vendorInfo != null)
                    {
                        obj.CategoryId = vendorInfo.CategoryId;
                        obj.CloseTime = vendorInfo.CloseTime;
                        obj.MobileNo = vendorInfo.MobileNo;
                        obj.OpenTime = vendorInfo.OpenTime;
                        obj.PersonName = vendorInfo.PersonName;
                        obj.UserId = vendorInfo.UserId;
                        obj.VendorRegistrationId = vendorInfo.VendorRegistrationId;
                        obj.WhatsappNo = vendorInfo.WhatsappNo;
                    }
                    returnObj.Data = obj;
                    returnObj.isSuccess = true;
                    returnObj.Message = "Login Successfully";
                }
                else
                {
                    returnObj.isSuccess = false;
                    returnObj.Message = "Login UnSuccessfull";
                }
            }
            catch (Exception ex)
            {
                Common common = new Common();
                common.ExeceptionHandleWithMethodName(ex, "Login,  VendorLogin");
                returnObj.Message = ex.Message;
                returnObj.isSuccess = false;
                return returnObj;
            }
            return returnObj;
        }

        //[HttpPost]
        //public ResponseObj<Customer> LoginCustomer(string MobileNo)
        //{
        //    ResponseObj<Customer> returnObj = new ResponseObj<Customer>();
        //    CustomerLogin obj = new CustomerLogin();
        //    OTPMobileVerification Otpobj = new OTPMobileVerification();
        //    Customer Cust = new Customer();

        //    try
        //    {
        //        var CustomerInfo = customerBs.getCustomerByMobile(MobileNo);

        //        if (CustomerInfo == null)
        //        {

        //            string mobileVerification = ConfigurationManager.AppSettings["MobileVerification"];
        //            string OTP = string.Empty;

        //            if (mobileVerification != "Test")
        //            {
        //                OTP = objCommon.GenerateRandonPassword("0123456789", 4);
        //            }
        //            else
        //            {
        //                OTP = "1111";
        //            }
        //            Otpobj.OTPMobileNo = MobileNo;
        //            Otpobj.OTP = OTP;

        //            customerBs.InsertOTPMobileVerification(Otpobj);
                    
        //                Otpobj.OTP = null;
        //                returnObj.isSuccess = true;
        //                returnObj.Message = "Otp Sent Successfully";

        //                Cust.MobileNo = MobileNo;
        //                customerBs.InsertCustomer(Cust);

        //            returnObj.isSuccess = true;
        //            returnObj.Message = "Customer Inserted Successfully";
        //            returnObj.Data = Cust;
        //        }
        //        else
        //        {
        //            string mobileVerification = ConfigurationManager.AppSettings["MobileVerification"];
        //            string OTP = string.Empty;

        //            if (mobileVerification != "Test")
        //            {
        //                OTP = objCommon.GenerateRandonPassword("0123456789", 4);
        //            }
        //            else
        //            {
        //                OTP = "1111";
        //            }
        //            Otpobj.OTPMobileNo = MobileNo;
        //            Otpobj.OTP = OTP;

        //            customerBs.InsertOTPMobileVerification(Otpobj);

        //            Otpobj.OTP = null;
        //            returnObj.isSuccess = true;
        //            returnObj.Message = "Otp Sent Successfully";
        //            Cust.MobileNo = MobileNo;
        //            returnObj.isSuccess = true;
        //            returnObj.Message = "Customer Inserted Successfully";
        //            returnObj.Data = Cust;
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        Common common = new Common();
        //        common.ExeceptionHandleWithMethodName(ex, "Login,  CustomerLogin");
        //        returnObj.Message = ex.Message;
        //        returnObj.isSuccess = false;
        //        return returnObj;
        //    }
        //    return returnObj;
        //}

        //[HttpPost]
        //public ResponseObj<string> SubmitOtp(String Otp)
        //{
        //    ResponseObj<string> returnObj = new ResponseObj<string>();
        //    string Otp = 
        //}

        [HttpPost]
        public ResponseObj<Customer> CustomerLogin(string MobileNo)
        {
            ResponseObj<Customer> returnObj = new ResponseObj<Customer>();
            Customer Cust = new Customer();

            try
            {
                var CustomerInfo = customerBs.getCustomerByMobileandPassword(MobileNo);
                if(CustomerInfo != null)
                {


                    returnObj.Data = CustomerInfo;
                    returnObj.isSuccess = true;
                    returnObj.Message = "Login Succesfully";
                }
                else
                {
                    returnObj.isSuccess = false;
                    returnObj.Message = "Login UnSuccesfully";
                }

            }
            catch (Exception ex)
            {
                Common common = new Common();
                common.ExeceptionHandleWithMethodName(ex, "Login,  CustomerLogin");
                returnObj.Message = ex.Message;
                returnObj.isSuccess = false;
                return returnObj;
            }
            return returnObj;
        }

    }
}
