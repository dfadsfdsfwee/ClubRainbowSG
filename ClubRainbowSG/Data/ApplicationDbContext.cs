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

        public DbSet<Registration> Registration { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<TestProgramme>()
        .HasKey(tp => new { tp.pcscode, tp.session_name }); // Composite Key

            modelBuilder.Entity<Registration>(entity =>
            {
                entity.HasKey(r => r.Id);
                entity.Property(r => r.programmePCS_FK).HasColumnName("programmePCS_FK");
                entity.Property(r => r.programmeSession_name_FK).HasColumnName("programmeSession_name_FK");
            });
            modelBuilder.Entity<Registration>()
                   .HasOne<TestProgramme>()
                   .WithMany()
                   .HasForeignKey(r => new { r.programmePCS_FK, r.programmeSession_name_FK })
                   .HasPrincipalKey(tp => new { tp.pcscode, tp.session_name })
                   .HasConstraintName("FK_Registration_TestProgram");
        }
    }


}
