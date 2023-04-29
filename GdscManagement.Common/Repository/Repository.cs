using System.Diagnostics.CodeAnalysis;
using GdscManagement.Common.Features.Base;
using Microsoft.EntityFrameworkCore;

namespace GdscManagement.Common.Repository;

public class BaseRepository<T> : IBaseRepository<T> where T : class, IModel
{
    private readonly DbContext _context;

    public BaseRepository(DbContext context)
    {
        _context = context;
        DbSet = _context.Set<T>();
    }

    public DbSet<T> DbSet { get; init; }

    public async Task<T> AddAsync([NotNull] T entity)
    {
        var added = (await DbSet.AddAsync(entity)).Entity;
        await Save();

        return added;
    }

    public async Task<IEnumerable<T>> GetAsync()
    {
        return await DbSet.ToListAsync();
    }

    public async Task<T?> GetAsync(string id)
    {
        return await DbSet.FirstOrDefaultAsync(e => e.Id == id);
    }

    public async Task<T?> UpdateAsync(string id, object newEntity)
    {
        var entity = await GetAsync(id);
        if (entity is null)
        {
            return null;
        }

        CheckUpdateObject(entity, newEntity);
        entity.Updated = DateTime.UtcNow;

        await Save();

        return DbSet.First(e => e.Id == id);
    }

    public async Task<T?> UpdateAsync(T entity)
    {
        var existingEntity = await GetAsync(entity.Id);
        if (existingEntity is null)
        {
            return null;
        }

        entity.Updated = DateTime.UtcNow;

        await Save();

        return DbSet.First(e => e.Id == entity.Id);
    }

    public async Task<T?> DeleteAsync(string id)
    {
        var entity = await DbSet.FirstOrDefaultAsync(item => item.Id == id);

        if (entity is null)
        {
            return null;
        }

        var removed = DbSet.Remove(entity).Entity;
        await Save();

        return removed;
    }

    public async Task<List<T>> DeleteAsync(string[] ids)
    {
        var entities = await DbSet.Where(item => ids.Contains(item.Id)).ToListAsync();
        entities.ForEach(entity => DbSet.Remove(entity));

        await Save();

        return entities;
    }

    private Task<int> Save()
    {
        return _context.SaveChangesAsync();
    }

    private static void CheckUpdateObject(T original, object updated)
    {
        foreach (var property in updated.GetType().GetProperties())
        {
            var value = property.GetValue(updated, null);
            var originalProp = original.GetType().GetProperty(property.Name);
            if (value is not null && originalProp is not null)
            {
                originalProp.SetValue(original, value);
            }
        }
    }
}
