using BOL;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class Context : DbContext
    {
        public Context() : base("EzDeliveryDB")
        {
            Database.SetInitializer(new MigrateDatabaseToLatestVersion<Context, DAL.Migrations.Configuration>());
            Configuration.ProxyCreationEnabled = false;
        }
        public DbSet<Customer> Customer { get; set; }
        public DbSet<Lookup_City> Lookup_Cities { get; set; }
        public DbSet<AgentRegistration> AgentRegistration { get; set; }
        public DbSet<Category> Category { get; set; }
        public DbSet<CustomerOrder> CustomerOrder { get; set; }
        public DbSet<DeliveryHeroRegistration> DeliveryHeroRegistration { get; set; }
        public DbSet<Lookup_Country> Lookup_Country { get; set; }
        public DbSet<Jobs> Jobs { get; set; }
        public DbSet<Lookup_OrderStatus> Lookup_OrderStatus { get; set; }
        public DbSet<Lookup_PaymentMode> Lookup_PaymentMode { get; set; }
        public DbSet<User> User { get; set; }
        public DbSet<Role> Role { get; set; }
        public DbSet<VendorRegistration> VendorRegistration { get; set; }
        public DbSet<OTPMobileVerification> OTPMobileVerification { get; set; }
        public DbSet<OrderInVoice> OrderInVoice { get; set; }
        public DbSet<CustomerAddress> CustomerAddress { get; set; }
        public DbSet<Lookup_AddressTypes> Lookup_AddressTypes { get; set; }
        public DbSet<OrderDriver> OrderDrivers { get; set; }
        public DbSet<OrderStatus> OrderStatus { get; set; }
        public DbSet<OrderStatusLog> OrderStatusLogs { get; set; }
    }
}
