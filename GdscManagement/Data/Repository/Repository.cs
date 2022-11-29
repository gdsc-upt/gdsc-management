using System.Diagnostics.CodeAnalysis;
using GdscRecruitment.Common.Features.Base;
using Microsoft.EntityFrameworkCore;

namespace GdscManagement.Data.Repository;

public class Repository<T> : IRepository<T> where T : class, IModel
{
    private readonly ApplicationDbContext _context;

    public Repository(ApplicationDbContext context)
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
