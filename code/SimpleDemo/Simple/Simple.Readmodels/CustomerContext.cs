using System.Data.Entity;
using Simple.Domain.QueryModels;

namespace Simple.Readmodels
{
    public class CustomerContext : DbContext
    {
        public CustomerContext() : base("Customers") {}

        public DbSet<CustomerDetails> CustomerDetails { get; set; }
        public DbSet<CustomerForList> CustomerForLists { get; set; }
    }
}