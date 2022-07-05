namespace ElearningDemoRepositories.Repositories;

public class MemberRepository : BaseRepository<Member>, IMemberRepository
{
    public MemberRepository(ElearningDemoContext db) : base(db)
    {
    }

    public override async Task<IEnumerable<Member>?> GetAllAliveAsync()
    {
        return await _db.Members
            .Where(m => m.IsDeleted == false)
            .ToListAsync();
    }

    public override async Task<IEnumerable<Member>?> GetAllValidAsync()
    {
        return await _db.Members
            .Where(m => m.IsValid == true)
            .Where(m => m.IsDeleted == false)
            .ToListAsync();
    }

    public override void SoftDelete(int id)
    {
        var Member = _db.Members.SingleOrDefault(m => m.Id == id);

        if (Member != null)
        {
            Member.IsDeleted = true;
            this.Update(Member);
        }
    }
}
