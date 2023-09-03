using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options; 

namespace homework1.Data
{
    public class ApplicationDbContext:DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) :base (options)
        {
            
        }
        public DbSet <products> products { get; set; }
        public object productDetails { get; internal set; }
    }
}
