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

    public EfUnitOfWork(
      DatabaseContext context,
      Func<DatabaseContext, IEmployerRepository> employersRepositoryFunc,
      Func<DatabaseContext, IWorkerRepository> workerRepositoryFunc
      )
    {
      _context = context;
      Employers = employersRepositoryFunc(_context);
      Workers = workerRepositoryFunc(_context);

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

    public string Connection
    {
      get => _context.Database.ProviderName;
    }
  }
}