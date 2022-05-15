using BOL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EzDeliveryAPI.Models
{
    public class ResponseObj<T> where T : class
    {
        public bool isSuccess { get; set; }
        public string Message { get; set; }
        public T Data { get; set; }
    }
    public class AgentModel
    {
        public int AgentRegistrationId { get; set; }
        public string AgentName { get; set; }
        public string MobileNo { get; set; }
        public string WhatsappNo { get; set; }
        public string AdharCard { get; set; }
        public string DOB { get; set; }
        public string Location { get; set; }
        public string HireDate { get; set; }
        public string JoinDate { get; set; }
        public int UserId { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
    }
    public class DeliveryHeroModel
    {
        public int DeliveryHeroRegistrationId { get; set; }
        public string HeroName { get; set; }
        public string MobileNo { get; set; }
        public string WhatsappNo { get; set; }
        public string AdharCard { get; set; }
        public string DrivingLicense { get; set; }
        public string AvailabilitySlot { get; set; }
        public string DOB { get; set; }
        public string Location { get; set; }
        public int AgentRegistrationId { get; set; }
        public int UserId { get; set; }
        public int CreatedBy { get; set; }
        public string UserName { get; set; }
        //public string Password { get; set; }
    }
    public class VendorModel
    {
        public int VendorRegistrationId { get; set; }
        public int CategoryId { get; set; }
        public string OpenTime { get; set; }
        public string CloseTime { get; set; }
        public string PersonName { get; set; }
        public string WhatsappNo { get; set; }
        public string MobileNo { get; set; }
        public bool isContractSign { get; set; }
        public int UserId { get; set; }
        public int CreatedBy { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public int RoleId { get; set; }
    }
    public class CustomerModel
    {
        public int CustomerId { get; set; }
        public string EmailId { get; set; }
        public string MobileNo { get; set; }
        public string WhatsappNo { get; set; }
        public string Name { get; set; }
        public string DOB { get; set; }
        public int GenderId { get; set; }
        public int CountryId { get; set; }
        public int CityId { get; set; }
        public string Address { get; set; }
        public string Zipcode { get; set; }
        public int CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public bool IsDelete { get; set; }
        public bool IsActive { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public int RoleId { get; set; }
        public string OTP { get; set; }
        public string FromNumber { get; set; }
        public string ToNumber { get; set; }
    }
    public class AgentLogin
    {
        public int AgentRegistrationId { get; set; }
        public string AgentName { get; set; }
        public string MobileNo { get; set; }
        public string WhatsappNo { get; set; }
        public string AdharCard { get; set; }
        public string DOB { get; set; }
        public string Location { get; set; }
        public string HireDate { get; set; }
        public string JoinDate { get; set; }
        public int UserId { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public int RoleId { get; set; }
        public string RoleName { get; set; }
    }


   

    public class HeroLogin
    {
        public int DeliveryHeroRegistrationId { get; set; }
        public string HeroName { get; set; }
        public string MobileNo { get; set; }
        public string WhatsappNo { get; set; }
        public string AdharCard { get; set; }
        public string DrivingLicense { get; set; }
        public string AvailabilitySlot { get; set; }
        public string DOB { get; set; }
        public string Location { get; set; }
        public int AgentRegistrationId { get; set; }
        public int UserId { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public int RoleId { get; set; }
        public string RoleName { get; set; }


    }
    public class CustomerLogin
    {
        public int CustomerId { get; set; }
        public string EmailId { get; set; }
        public string MobileNo { get; set; }
        public string WhatsappNo { get; set; }
        public string Name { get; set; }
        public string DOB { get; set; }
        public int GenderId { get; set; }
        public int CountryId { get; set; }
        public int CityId { get; set; }
        public string Address { get; set; }
        public string Zipcode { get; set; }
        public int UserId { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public int RoleId { get; set; }
        public string RoleName { get; set; }
    }

    public class VendorLogin
    {
        public int VendorRegistrationId { get; set; }
        public int CategoryId { get; set; }
        public string OpenTime { get; set; }
        public string CloseTime { get; set; }
        public string PersonName { get; set; }
        public string WhatsappNo { get; set; }
        public string MobileNo { get; set; }
        public int UserId { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public int RoleId { get; set; }
        public string RoleName { get; set; }
    }
    public class CustomerOrderModel
    {
        public Customer Customer { get; set; }
        public CustomerAddress PickupPoint { get; set; }
        public CustomerAddress DeliveryPoint { get; set; }
        public CustomerOrder CustomerOrder { get; set; }
        
    }
    public class OrderDriverModel
    {
        public int CustomerOrderId { get; set; }
        public int DriverId { get; set; }
        public decimal BidPrice { get; set; }
        public string Time { get; set; }
        public string DeliveryNotes { get; set; }

        public bool IsSelected { get; set; }
    }
    public class UpdateCustomerModel
    {
        public int CustomerId { get; set; }
        public string EmailId { get; set; }
        public string MobileNo { get; set; }
        public string Password { get; set; }
        public string WhatsappNo { get; set; }
        public string Name { get; set; }
        public string DOB { get; set; }
        public int GenderId { get; set; }
    }

    public class CustOrderModel
    {
        public CustomerOrder CustomerOrder { get; set; }
        public List<CustomerAddress> CustomerAddress { get; set; }
    }

    public class OrderStatusModel
    {
        public OrderStatus orderStatus { get; set; }
        public OrderStatusLog orderStatusLog { get; set; }
    }

    public class CustomerRegisterModel
    {   
       public string MobileNo { get; set; }
       public string Otp { get; set; }
       public string Password { get; set; }
    }

    public class OrderDriverDriverModel
    {
        public OrderDriver OrderDriver { get; set; }

        public DeliveryHeroRegistration Driver { get; set; }

    }

   

}