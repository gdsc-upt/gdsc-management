using System.Diagnostics.CodeAnalysis;
using GdscManagement.Common.Features.Base;
using GdscManagement.Common.Features.Projects.Models;
using Microsoft.EntityFrameworkCore;

namespace GdscManagement.Common.Repository;

public interface IBaseRepository<T> where T : IModel
{
   // DbSet<T> DbSet { get; init; }

    Task<T> AddAsync([NotNull] T entity);
    Task SaveAsync();

    Task<T?> GetAsync(string id);

    Task<IEnumerable<T>> GetAsync();

    Task<T?> UpdateAsync(string id, object newEntity);
    Task<T?> UpdateAsync(T entity);

    Task<T?> DeleteAsync(string id);
    Task<List<T>> DeleteAsync(string[] id);
}

public interface IRepository<T>: IBaseRepository<T> where T : class, IModel
{
    DbSet<T> DbSet { get; init; }
}
