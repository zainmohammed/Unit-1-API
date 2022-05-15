using BOL;
using DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
   public class LoginBs
    {
        private LoginDb objDb;
        public LoginBs()
        {
            objDb = new LoginDb();
        }
        public User Login(string UserName, string Password)
        {
            return objDb.Login(UserName, Password);
        }
        public Role GetRolesById(int roleId)
        {
            return objDb.GetRolesById(roleId);
        }

        public DeliveryHeroRegistration HeroLogin(string Mobile )
        {
            return objDb.HeroLogin(Mobile);
        }
    }
}
