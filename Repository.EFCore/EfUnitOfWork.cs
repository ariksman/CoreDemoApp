using System;
using Microsoft.EntityFrameworkCore;
using Repository.Core;
using Repository.Core.Repositories;
using Repository.EFCore.Repositories;

namespace Repository.EFCore
{
  public class EfUnitOfWork : IUnitOfWork
  {
    private readonly DatabaseContext _context;

    public EfUnitOfWork(string dataSource = null)
    {
      if (dataSource == null) dataSource = DatabaseConnectionFactory.GetInstallDirectory();
      var options = DatabaseConnectionFactory.CreateConnectionString(dataSource);
      _context = new DatabaseContext(options);

      Employers = new EmployerRepository(_context);
      Workers = new WorkerRepository(_context);

      Migrate();
    }

    public void Dispose()
    {
      Dispose(true);
      GC.SuppressFinalize(this);
    }

    protected virtual void Dispose(bool disposing)
    {
      if (disposing)
      {
        _context?.Dispose();
      }
    }

    public IEmployerRepository Employers { get; }
    public IWorkerRepository Workers { get; }

    public int Complete()
    {
      return _context.SaveChanges();
    }

    public void Migrate()
    {
      try
      {
        _context.Database.EnsureCreated();
      }
      catch (Exception ex)
      {
        Console.WriteLine(ex);
      }

      try
      {
        _context.Database.Migrate();
      }
      catch (Exception ex)
      {
        Console.WriteLine(ex);
      }
    }
  }
}