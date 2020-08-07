using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace CustomerManagement.Data.PostgreSqlDbContext
{
    public class ApplicationDbContextFactory : IDesignTimeDbContextFactory<ApplicationDbContext>
    {
        public ApplicationDbContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<ApplicationDbContext>();
            optionsBuilder.UseNpgsql("Host=customer-management-postgres.c0d8nzigxluc.eu-west-2.rds.amazonaws.com;Database=customerdb;Username=postgres;Password=SDFwer741");

            return new ApplicationDbContext(optionsBuilder.Options);
        }
    }
}