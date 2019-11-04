using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using Repository.Core.Repositories;

namespace Repository.EFCore.Repositories
{
  public class Repository<TEntity> : IRepository<TEntity> where TEntity : class
  {
    protected readonly DbContext Context;
    internal DbSet<TEntity> DbSet;

    protected Repository(DatabaseContext context)
    {
      Context = context;
      DbSet = context.Set<TEntity>();
    }

    public IEnumerable<TEntity> Get(Expression<Func<TEntity, bool>> filter = null,
      Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
      string includeProperties = "")
    {
      IQueryable<TEntity> query = DbSet;
      if (filter != null) query = query.Where(filter);

      query = includeProperties
        .Split(new[] {','}, StringSplitOptions.RemoveEmptyEntries)
        .Aggregate(query, (current, includeProperty) => current.Include(includeProperty));

      return orderBy != null ? orderBy(query).ToList() : query.ToList();
    }

    public TEntity GetById(Guid id)
    {
      return DbSet.Find(id);
    }

    public IEnumerable<TEntity> GetAll()
    {
      return DbSet.AsNoTracking().ToList();
    }

    public void Add(TEntity entity)
    {
      throw new NotImplementedException();
    }

    public void AddRange(IEnumerable<TEntity> entities)
    {
      throw new NotImplementedException();
    }

    public void Remove(TEntity entity)
    {
      throw new NotImplementedException();
    }

    public void RemoveRange(IEnumerable<TEntity> entities)
    {
      throw new NotImplementedException();
    }

    public void Load()
    {
      throw new NotImplementedException();
    }

    public void Clear()
    {
      Context.RemoveRange(DbSet.ToList());
    }

    public int Count()
    {
      throw new NotImplementedException();
    }
  }
}