using BOL;
using DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
   public class OrderStatusLogBs
    {
        private OrderStatusLogDb ObjDb;

        public OrderStatusLogBs()
        {
            ObjDb = new OrderStatusLogDb();
        }
        public bool InsertOrderStatusLog(OrderStatusLog orderStatusLog)
        {
            try
            {
                ObjDb.InsertOrderStatusLog(orderStatusLog);
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public List<OrderStatusLog> GetOrderStatusById(int id)
        {
            return ObjDb.GetOrderStatusById(id);
        }

        public OrderStatus GetOrderStatusId(int id)
        {
            return ObjDb.GetOrderStatusId(id);
        }
    }
}
