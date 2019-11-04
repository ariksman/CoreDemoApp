using Microsoft.EntityFrameworkCore;
using Repository.Core.DataModel;

namespace Repository.EFCore
{
  public sealed class DatabaseContext : DbContext
  {
    public DatabaseContext(DbContextOptions options)
      : base(options)
    {
    }

    public DbSet<Employer> Employers { get; set; }
    public DbSet<Worker> Workers { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
      //optionsBuilder.UseSqlite("Data Source=test.db");
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
      base.OnModelCreating(modelBuilder);

      modelBuilder.Entity<Employer>()
        .HasMany(employer => employer.Workers)
        .WithOne(worker => worker.Employer1);

      modelBuilder.Entity<Worker>()
        .Property(w => w.WorkerId)
        .ValueGeneratedNever();

      modelBuilder.Entity<Employer>()
        .Property(w => w.EmployerId)
        .ValueGeneratedNever();
    }
  }
}