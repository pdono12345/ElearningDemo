namespace ElearningDemoRepositories.Repositories;

public class TeacherRepository : BaseRepository<Teacher>, ITeacherRepository
{
    public TeacherRepository(ElearningDemoContext db) : base(db)
    {
    }

    public override async Task<IEnumerable<Teacher>?> GetAllAliveAsync()
    {
        return await _db.Teachers
            .Where(m => m.IsDeleted == false)
            .ToListAsync();
    }

    public override async Task<IEnumerable<Teacher>?> GetAllValidAsync()
    {
        return await _db.Teachers
            .Where(m => m.IsValid == true)
            .Where(m => m.IsDeleted == false)
            .ToListAsync();
    }

    public override void SoftDelete(int id)
    {
        var Teacher = _db.Teachers.SingleOrDefault(m => m.Id == id);

        if (Teacher != null)
        {
            Teacher.IsDeleted = true;
            this.Update(Teacher);
        }
    }
}
