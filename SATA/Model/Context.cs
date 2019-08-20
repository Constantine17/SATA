using Microsoft.EntityFrameworkCore;

namespace SATA.Model
{
    public class Context : DbContext
    {
        public Context(DbContextOptions<Context> options) : base(options)
        { }
        public DbSet<Customer> Customer { get; set; }

        public DbSet<Transaction> Transaction { get; set; }
    }
}
