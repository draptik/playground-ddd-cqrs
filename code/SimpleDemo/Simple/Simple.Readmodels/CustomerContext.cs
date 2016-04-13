using System.Data.Entity;

namespace Simple.Readmodels
{
    public class CustomerContext : DbContext
    {
        public CustomerContext() : base("Customers")
        {
        }

        public DbSet<Customer> Customers { get; set; }
        public DbSet<CustomerForList> CustomerForLists { get; set; }
    }
}