namespace ElearningDemoRepositories.Repositories;
public class BaseRepository<TEntity> : IBaseRepository<TEntity> where TEntity : class
{
    protected readonly ElearningDemoContext _db;

    public BaseRepository(ElearningDemoContext db)
    {
        _db = db;
    }

    public virtual void Create(TEntity tEntity)
    {
        _db.Set<TEntity>().Add(tEntity);
    }
    public virtual void Update(TEntity tEntity)
    {
        _db.Set<TEntity>().Update(tEntity);
    }

    public virtual void SoftDelete(int id)
    {
        throw new NotImplementedException();
    }
    public virtual void HardDelete(TEntity tEntity)
    {
        _db.Set<TEntity>().Remove(tEntity);
    }
    public virtual IQueryable<TEntity> Query()
    {
        return _db.Set<TEntity>();
    }
    public virtual async Task<IEnumerable<TEntity>?> GetAllAsync()
    {
        return await _db.Set<TEntity>().ToListAsync();
    }
    public virtual IEnumerable<TEntity> GetAll()
    {
        return _db.Set<TEntity>();
    }

    public virtual TEntity? GetOne(Expression<Func<TEntity, bool>> condition)
    {
        return _db.Set<TEntity>().AsNoTracking().FirstOrDefault(condition);
    }

    public virtual async Task<TEntity?> GetOneAsync(Expression<Func<TEntity, bool>> condition)
    {
        return await _db.Set<TEntity>().AsNoTracking().FirstOrDefaultAsync(condition);
    }

    public void SaveChange()
    {
        _db.SaveChanges();
    }

    public async Task SaveChangeAsync()
    {
        await _db.SaveChangesAsync();
    }
    public void DetachedTrackByCondition(Func<TEntity, bool> condition)
    {
        var existingEntity = _db.Set<TEntity>().Local.FirstOrDefault(condition);
        if (existingEntity != null) { _db.Entry(existingEntity).State = EntityState.Detached; }
    }

    public virtual Task<IEnumerable<TEntity>?> GetAllValidAsync()
    {
        throw new NotImplementedException();
    }

    public virtual Task<IEnumerable<TEntity>?> GetAllAliveAsync()
    {
        throw new NotImplementedException();
    }
}

