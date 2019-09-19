﻿using System.IO;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace Repository.EFCore
{
  public class DatabaseConnectionFactory : IDesignTimeDbContextFactory<DatabaseContext>
  {
    public DatabaseConnectionFactory()
    {

    }

    public DatabaseContext CreateDbContext(string[] args)
    {
      var dir = GetInstallDirectory();
      return new DatabaseContext(CreateConnectionString(dir));
    }

    private string GetInstallDirectory()
    {
      var installationDirectory = System.AppDomain.CurrentDomain.BaseDirectory;
      var filePath = Path.Combine(installationDirectory, "localDb.sqlite");

      return filePath;
    }

    public static DbContextOptions CreateConnectionString(string dataSource)
    {
      var connectionStringBuilder = new SqliteConnectionStringBuilder { DataSource = dataSource };
      var connectionString = connectionStringBuilder.ToString();
      var connection = new SqliteConnection(connectionString);

      var optionsBuilder = new DbContextOptionsBuilder();
      optionsBuilder.UseSqlite(connection);
      //optionsBuilder.UseLazyLoadingProxies();
      return optionsBuilder.Options;
    }
  }
}