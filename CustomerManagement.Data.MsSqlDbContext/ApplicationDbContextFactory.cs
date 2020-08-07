using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace CustomerManagement.Data.MsSqlDbContext
{

    public class ApplicationDbContextFactory : IDesignTimeDbContextFactory<ApplicationDbContext>
    {
        public ApplicationDbContext CreateDbContext(string[] args)
        {
            string con =
                "Data Source=customer-management-db.c0d8nzigxluc.eu-west-2.rds.amazonaws.com;" +
                "Initial Catalog=customerdb;" +
                "User id=admin;" +
                "Password=SDFwer741;";

            var optionsBuilder = new DbContextOptionsBuilder<ApplicationDbContext>();
            optionsBuilder.UseSqlServer(con, providerOptions=>providerOptions.CommandTimeout(60))
                .UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);

            return new ApplicationDbContext(optionsBuilder.Options);
        }
    }


}