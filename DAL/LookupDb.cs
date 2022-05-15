using BOL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
   public class LookupDb
    {
        private Context db;
        public LookupDb()
        {
            db = new Context();
        }
        public List<Category> GetAllCategory()
        {
            return db.Category.ToList();
        }
        public List<Lookup_Country> GetAllCountry()
        {
            return db.Lookup_Country.ToList();
        }
        public List<Lookup_City> GetCitiesByCountry(int countryId)
        {
            return db.Lookup_Cities.Where(x=>x.CountryId== countryId).ToList();
        }
        public List<Lookup_OrderStatus> GetAllJobStatus()
        {
            return db.Lookup_OrderStatus.ToList();
        }
    }
}
