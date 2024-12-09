using ClubRainbowSG.Models;
using Microsoft.EntityFrameworkCore;
namespace clubrainbowSG.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

          
        }
        public DbSet<User> Account { get; set; }
        public DbSet<TestProgramme> TestProgram { get; set; }
       public DbSet<Contacts> Contacts { get; set; }
    }

  
}
