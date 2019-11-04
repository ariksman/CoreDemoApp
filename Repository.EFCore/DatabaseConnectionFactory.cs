using System;
using System.IO;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace Repository.EFCore
{
  public class DatabaseConnectionFactory : IDesignTimeDbContextFactory<DatabaseContext>
  {
    public DatabaseContext CreateDbContext(string[] args)
    {
      var dir = GetInstallDirectory();
      return new DatabaseContext(CreateConnectionString(dir));
    }

    public static string GetInstallDirectory()
    {
      var installationDirectory = AppDomain.CurrentDomain.BaseDirectory;
      var filePath = Path.Combine(installationDirectory, "localDb.sqlite");

      return filePath;
    }

    public static DbContextOptions CreateConnectionString(string dataSource)
    {
      var connectionStringBuilder = new SqliteConnectionStringBuilder {DataSource = dataSource};
      var connectionString = connectionStringBuilder.ToString();
      var connection = new SqliteConnection(connectionString);

      var optionsBuilder = new DbContextOptionsBuilder();
      optionsBuilder.UseSqlite(connection);
      //optionsBuilder.UseLazyLoadingProxies();
      return optionsBuilder.Options;
    }
  }
}