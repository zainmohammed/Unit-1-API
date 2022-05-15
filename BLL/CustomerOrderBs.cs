using BOL;
using DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static DAL.ModelDb;

namespace BLL
{
   public class CustomerOrderBs
    {
        private CustomerOrderDb objDb;
        public CustomerOrderBs()
        {
            objDb = new CustomerOrderDb();
        }

        public List<string> Errors = new List<string>();

        public bool InsertCustomerOrder(CustomerOrder cusorder)
        {
            try
            {
                objDb.InsertCustomerOrder(cusorder);
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
        public bool InsertOrderDriver(OrderDriver orderDriver)
        {
            try
            {
                objDb.InsertOrderDriver(orderDriver);
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
        public bool UpdateCustomerOrder(CustomerOrder obj)
        {
            try
            {
                objDb.UpdateCustomerOrder(obj);
                return true;

            }
            catch (Exception ex)
            {
                Errors.Add(ex.Message);
                return false;
            }
        }

        public List<CustomerOrder> GetCustomerOrderByCustomerAddress(int id)
        {
            return objDb.GetCustomerOrderByCustomerAddress(id);
        }
        public List<CustomerOrder> GetOrderByCustomerId(int CustomerId)
        {
            return objDb.GetOrderByCustomerId(CustomerId);
        }

        public List<CustomerOrderDetailsModel> GetOrdersForHeroByStatus(string OrderDate, int HeroId,  int Status)
        {
            return objDb.GetOrdersForHeroByStatus(OrderDate,HeroId, Status);
        }

        public CustomerOrder GetCustomerOrderById(int Id)
        {
            return objDb.GetCustomerOrderById(Id);
        }
        public CustomerOrder RejectBid(int Id)
        {
            return objDb.BidReject(Id);
        }
      
        public List<CustomerOrder> GetListofCustomerOrder(int vendorId)
        {
            return objDb.GetListofCustomerOrder(vendorId);
        }
        public OrderDriver GetCustomerOrderId(int OrderDriverId)
        {
            return objDb.GetCustomerOrderId(OrderDriverId);
        }

      

        public OrderDriver getOrderDriverByOrderDriver(int OrderDriverId)
        {
            return objDb.getOrderDriverByOrderDriver(OrderDriverId);
        }

        public List<OrderDriver> GetMinBidPrice(int CustomerOrderId)
        {
            return objDb.GetMinBidPrice(CustomerOrderId);
        }
        public List<CustomerOrder> GetAllCustomerOrders()
        {
            return objDb.GetAllCustomerOrders();
        }

        public List<CustomerOrder> PendingOrders(int Status)
        {
            return objDb.PendingOrders(Status);
        }

        public List<CustomerOrderDetailsModel> GetCustomerOrderList(DateTime orderDateTime, int HeroId)
        {
            return objDb.GetCustomerOrderList(orderDateTime, HeroId);
        }

        public List<CustomerOrderDetailsModel> GetcompleteCustomerOrderListByUserId(int OrderDriverId)
        {
            return objDb.GetcompleteCustomerOrderListByUserId(OrderDriverId);
        }
        
        public CustomerOrderDetailsModel GetCustomerOrderDetails(int CustomerOrderId)
        {
            return objDb.GetCustomerOrderDetails(CustomerOrderId);
        }
    }
}
