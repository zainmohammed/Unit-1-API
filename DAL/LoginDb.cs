using BOL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
   public class LoginDb
    {
        private Context db;
        public LoginDb()
        {
            db = new Context();
        }
        public User Login(string UserName, string Password)
        {
           return db.User.Where(x => x.UserName == UserName && x.Password == Password).FirstOrDefault();
        }
        public Role GetRolesById(int roleId)
        {
            return db.Role.Where(x =>  x.RoleId == roleId).FirstOrDefault();
            
        }

        public DeliveryHeroRegistration HeroLogin(string Mobile )
        {
            return db.DeliveryHeroRegistration.Where(x => x.MobileNo == Mobile).FirstOrDefault();
        }
    }
}
