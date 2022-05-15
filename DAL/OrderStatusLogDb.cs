using BOL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
   public class OrderStatusLogDb
    {
        private Context db;

        public OrderStatusLogDb()
        {
            db = new Context();
        }

        public void InsertOrderStatusLog(OrderStatusLog orderStatusLog)
        {
            db.OrderStatusLogs.Add(orderStatusLog);
            Save();
        }
        public List<OrderStatusLog> GetOrderStatusById(int id)
        {
            return db.OrderStatusLogs.Where(x => x.OrderStatusLogId == id).ToList();
        }

        public OrderStatus GetOrderStatusId(int id)
        {
            return db.OrderStatus.Where(x => x.OrderStatusId == id).FirstOrDefault();
        }

        private void Save()
        {
            db.SaveChanges();
        }
    }
}
