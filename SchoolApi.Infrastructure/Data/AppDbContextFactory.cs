using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using SchoolApi.Infrastructure.Data;

namespace SchoolApi.Infrastructure.Data;

public class AppDbContextFactory : IDesignTimeDbContextFactory<AppDbContext>
{
    public AppDbContext CreateDbContext(string[] args)
    {
        var optionsBuilder = new DbContextOptionsBuilder<AppDbContext>();
        
        // GANTI koneksi ini sesuai database kamu
        optionsBuilder.UseNpgsql("Host=localhost;Database=schooldb;Username=postgres;Password=admin");

        return new AppDbContext(optionsBuilder.Options);
    }
}
