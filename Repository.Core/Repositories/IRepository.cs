using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace Repository.Core.Repositories
{
  public interface IRepository<TEntity> where TEntity : class
  {
    /// <summary>
    /// </summary>
    /// <param name="filter">
    ///   Lambda expression filter which return boolean.
    /// </param>
    /// <param name="orderBy">
    ///   Lambda expression which return ordered version of the object.
    /// </param>
    /// <param name="includeProperties">
    ///   Provides a comma-delimited list.
    /// </param>
    /// <returns></returns>
    IEnumerable<TEntity> Get(
      Expression<Func<TEntity, bool>> filter = null,
      Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
      string includeProperties = "");

    TEntity GetById(Guid id);

    /// <summary>
    ///   Implements the ToList() method with AsNoTracking() ! on the TEntity collection.
    /// </summary>
    /// <returns></returns>
    IEnumerable<TEntity> GetAll();

    /// <summary>
    ///   Implements the Add() functionality of the Entity Framework Core.
    /// </summary>
    void Add(TEntity entity);

    /// <summary>
    ///   Implements the AddRange() functionality of the Entity Framework Core.
    /// </summary>
    void AddRange(IEnumerable<TEntity> entities);

    /// <summary>
    ///   Implements the Remove() method on EF core
    /// </summary>
    void Remove(TEntity entity);

    /// <summary>
    ///   Implements the RemoveRange() method on EF core
    /// </summary>
    /// <param name="entities">
    ///   TEntity parameter which is used for the method.
    /// </param>
    void RemoveRange(IEnumerable<TEntity> entities);

    /// <summary>
    ///   Implements the Load() method on EF core.
    /// </summary>
    void Load();

    /// <summary>
    ///   Removes all rows in a table.
    /// </summary>
    void Clear();

    /// <summary>
    ///   Implements the Count() method on EF core.
    /// </summary>
    /// <returns></returns>
    int Count();
  }
}