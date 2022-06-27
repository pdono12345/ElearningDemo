namespace ElearningDemoRepositories.Repositories;

public class MenagerRepository : BaseRepository<Manager>, IManagerRepository
{
    public MenagerRepository(ElearningDemoContext db) : base(db)
    {
    }

    public override async Task<IEnumerable<Manager>> GetAllAliveAsync()
    {
        return await _db.Managers
            .Where(m => m.IsDeleted == false)
            .ToListAsync();
    }

    public override async Task<IEnumerable<Manager>> GetAllValidAsync()
    {
        return await _db.Managers
            .Where(m => m.IsValid == true)
            .Where(m => m.IsDeleted == false)
            .ToListAsync();
    }

    public override void SoftDelete(int id)
    {
        var manager = _db.Managers.SingleOrDefault(m => m.Id == id);

        if (manager != null)
        {
            manager.IsDeleted = true;
            this.Update(manager);
        }
    }
}
