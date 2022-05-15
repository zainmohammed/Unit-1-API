using BOL;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static DAL.ModelDb;

namespace DAL
{
   public class CustomerOrderDb
    {
        private Context db;
        public CustomerOrderDb()
        {
            db = new Context();
        }
        public void InsertCustomerOrder(CustomerOrder cusorder)
        {
            db.CustomerOrder.Add(cusorder);
            Save();
        }
        public List<CustomerOrder> GetCustomerOrderByCustomerAddress(int id)
        {
            return db.CustomerOrder.Where(x => x.CustomerAddressId == id).ToList();
        }
        public CustomerOrder GetCustomerOrderById(int Id)
        {
            return db.CustomerOrder.Where(x => x.CustomerOrderId == Id).FirstOrDefault();
        }
        public CustomerOrder BidReject(int Id)
        {
            return db.CustomerOrder.Where(x => x.CustomerOrderId == Id).FirstOrDefault();
        }
        //public CustomerOrder BidCancel(int Id)
        //{
        //    return db.CustomerOrder.Where(x => x.CustomerOrderId == Id).FirstOrDefault();
        //}
        //----------------------------------

        //----------------------------------------

        public List<CustomerOrder> GetListofCustomerOrder(int vendorId)
        {
            return db.CustomerOrder.Where(x => x.VendorRegistrationId == vendorId).ToList();
        }
        public List<CustomerOrder> GetOrderByCustomerId(int CustomerId)
        {
            return db.CustomerOrder.Where(x => x.CustomerId == CustomerId && x.IsOrderFlag == false).ToList();
        }

        public List<CustomerOrderDetailsModel> GetCustomerOrderList(DateTime orderDateTime,int HeroId)
        {
            List<CustomerOrderDetailsModel> customerOrderDetailsModels = new List<CustomerOrderDetailsModel>();
            SqlParameter pOrderDate = new SqlParameter("@OrderDate", orderDateTime);
            SqlParameter pHeroId = new SqlParameter("@HeroId", HeroId);
            customerOrderDetailsModels = db.Database.SqlQuery<CustomerOrderDetailsModel>("uspGetCustomerOrderList @OrderDate, @HeroId", pOrderDate, pHeroId).ToList();
            return customerOrderDetailsModels;
        }

        public List<CustomerOrderDetailsModel> GetcompleteCustomerOrderListByUserId(int OrderDriverId)
        {
            List<CustomerOrderDetailsModel> customerOrderDetailsModels = new List<CustomerOrderDetailsModel>();
            SqlParameter pOrderDriverId = new SqlParameter("@OrderDriverId", OrderDriverId);
            customerOrderDetailsModels = db.Database.SqlQuery<CustomerOrderDetailsModel>("uspGetcompleteCustomerOrderListByUserId @OrderDriverId", pOrderDriverId).ToList();
            return customerOrderDetailsModels;
        }

        
        public List<CustomerOrderDetailsModel> GetOrdersForHeroByStatus(string OrderDate, int HeroId, int Status)
        {
            SqlParameter pHeroId = new SqlParameter("@HeroId", HeroId);
            SqlParameter pOrderDate = new SqlParameter("@OrderDate", OrderDate);
            SqlParameter pStatus = new SqlParameter("@Status", Status);
            var customerOrderDetailsModels = db.Database.SqlQuery<CustomerOrderDetailsModel>("uspGetOrdersForHeroByStatus @OrderDate,@HeroId,@Status", pOrderDate, pHeroId, pStatus).ToList();

            return customerOrderDetailsModels;
        }

        public CustomerOrderDetailsModel GetCustomerOrderDetails(int CustomerOrderId)
        {
            CustomerOrderDetailsModel customerOrderDetailsModels = new CustomerOrderDetailsModel();
            SqlParameter pCustomerOrderId = new SqlParameter("@CustomerOrderId", CustomerOrderId);
            customerOrderDetailsModels = db.Database.SqlQuery<CustomerOrderDetailsModel>("uspGetCustomerOrderDetails @CustomerOrderId", pCustomerOrderId).FirstOrDefault();
            return customerOrderDetailsModels;
        }

        private void Save() 
        {
            db.SaveChanges();
        }

        public void InsertOrderDriver(OrderDriver orderDriver)
        {
            db.OrderDrivers.Add(orderDriver);
            Save();
        }

        public void UpdateCustomerOrder(CustomerOrder obj)
        {
            db.Entry(obj).State = EntityState.Modified;
            db.Configuration.ValidateOnSaveEnabled = false;
            Save();
            db.Configuration.ValidateOnSaveEnabled = true;
        }
        public OrderDriver GetCustomerOrderId(int OrderDriverId)
        {
            return db.OrderDrivers.Where(x => x.OrderDriverId == OrderDriverId).FirstOrDefault();
        }

        //public CustomerOrderDetailsModel CustomerOrderDetails(int CustomerOrderId)
        //{
        //    return db.CustomerAddress.Where(x => x.CustomerOrderId == CustomerOrderId).FirstOrDefault();
        //}



        public OrderDriver getOrderDriverByOrderDriver(int OrderDriverId)
        {
            return db.OrderDrivers.Where(x => x.OrderDriverId == OrderDriverId).FirstOrDefault();
        }
        public List<OrderDriver> GetMinBidPrice(int CustomerOrderId)
        {
            var l = db.OrderDrivers.Where(x=>x.CustomerOrderId == CustomerOrderId).ToList();

            var minPrice = (from res in l.AsEnumerable()
                            orderby res.BidPrice ascending
                            select new OrderDriver
                            {
                                Time=res.Time,
                                DeliveryNotes=res.DeliveryNotes,
                                OrderDriverId = res.OrderDriverId,
                                BidPrice = res.BidPrice,
                                CustomerOrderId = res.CustomerOrderId,
                                DriverId = res.DriverId,
                                Status=res.Status,
                                IsSelected = res.IsSelected,
                                CustomerOrder = res.CustomerOrderId > 0 ? db.CustomerOrder.Where(x=> x.CustomerOrderId==res.CustomerOrderId).FirstOrDefault(): null,
                                HeroRegistration =res.DriverId>0? db.DeliveryHeroRegistration.Where(x => x.DeliveryHeroRegistrationId == res.DriverId).FirstOrDefault():null

                            }).Take(5);
            return minPrice.ToList();
        }
        public List<CustomerOrder> GetAllCustomerOrders()
        {
            return db.CustomerOrder.ToList();
        }
        public List<CustomerOrder> PendingOrders(int Status)
        {

            return db.CustomerOrder.Where(x => x.OrderStatusId == Status).ToList();
        }
    }
}
