using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Options;

namespace Arztpraxis.Data
{
    // Factory used for migrations
    public class DesignTimeDbContextFactory : IDesignTimeDbContextFactory<DoctorsOfficeDbContext>
    {
        public DoctorsOfficeDbContext CreateDbContext(string[] args)
        {
            string connectionString = "Data Source=TESTVM-MAC\\SQLEXPRESS;Initial Catalog=DoctorsOffice;Integrated Security=True;Connect Timeout=30;Encrypt=True;Trust Server Certificate=True;";

            DbContextOptionsBuilder optionsBuilder = new DbContextOptionsBuilder<DoctorsOfficeDbContext>();
            optionsBuilder.UseSqlServer(connectionString);

            return new DoctorsOfficeDbContext(optionsBuilder.Options);
        }
    }
}
