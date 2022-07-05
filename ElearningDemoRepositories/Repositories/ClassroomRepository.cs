namespace ElearningDemoRepositories.Repositories;

public class ClassroomRepository : BaseRepository<Classroom>, IClassroomRepository
{
    public ClassroomRepository(ElearningDemoContext db) : base(db)
    {
    }

    public override async Task<IEnumerable<Classroom>?> GetAllAliveAsync()
    {
        return await _db.Classrooms
            .Where(m => m.IsDeleted == false)
            .ToListAsync();
    }

    public override async Task<IEnumerable<Classroom>?> GetAllValidAsync()
    {
        return await _db.Classrooms
            .Where(m => m.IsValid == true)
            .Where(m => m.IsDeleted == false)
            .ToListAsync();
    }

    public override void SoftDelete(int id)
    {
        var Classroom = _db.Classrooms.SingleOrDefault(m => m.Id == id);

        if (Classroom != null)
        {
            Classroom.IsDeleted = true;
            this.Update(Classroom);
        }
    }
}
