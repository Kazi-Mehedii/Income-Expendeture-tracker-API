using DataAccess.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace DataAccess
{
    public class Context : DbContext
    {
        public IConfiguration Configuration { get; }

        public Context(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public DbSet<Transaction> Transactions { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var connectionString = Configuration.GetConnectionString("defaultConnection");
            optionsBuilder.UseSqlServer(connectionString);
        }
    }
}
