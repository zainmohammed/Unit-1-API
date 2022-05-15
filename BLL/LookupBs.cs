using BOL;
using DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
   public class LookupBs
    {
        private LookupDb objDb;
        public LookupBs()
        {
            objDb = new LookupDb();
        }
        public List<Category> GetAllCategory()
        {
            return objDb.GetAllCategory();
        }
        public List<Lookup_Country> GetAllCountry()
        {
            return objDb.GetAllCountry();
        }
        public List<Lookup_City> GetCitiesByCountry(int countryId)
        {
            return objDb.GetCitiesByCountry(countryId);
        }
        public List<Lookup_OrderStatus> GetAllJobStatus()
        {
            return objDb.GetAllJobStatus();
        }
    }
}
