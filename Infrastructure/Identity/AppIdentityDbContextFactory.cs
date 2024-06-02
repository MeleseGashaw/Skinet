using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Infrastructure.Data.Identity; // Adjust the namespace to where your DbContext is located

public class AppIdentityDbContextFactory : IDesignTimeDbContextFactory<AppIdentityDBContext>
{
    public AppIdentityDBContext CreateDbContext(string[] args)
    {
        var optionsBuilder = new DbContextOptionsBuilder<AppIdentityDBContext>();
        // Ensure the connection string matches your configuration
        optionsBuilder.UseSqlServer("IdentityConnection");

        return new AppIdentityDBContext(optionsBuilder.Options);
    }
}