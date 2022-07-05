namespace ElearningDemoRepositories.Repositories;

public class StudentRepository : BaseRepository<Student>, IStudentRepository
{
    public StudentRepository(ElearningDemoContext db) : base(db)
    {
    }

    public override async Task<IEnumerable<Student>?> GetAllAliveAsync()
    {
        return await _db.Students
            .Where(m => m.IsDeleted == false)
            .ToListAsync();
    }

    public override async Task<IEnumerable<Student>?> GetAllValidAsync()
    {
        return await _db.Students
            .Where(m => m.IsValid == true)
            .Where(m => m.IsDeleted == false)
            .ToListAsync();
    }

    public override void SoftDelete(int id)
    {
        var Student = _db.Students.SingleOrDefault(m => m.Id == id);

        if (Student != null)
        {
            Student.IsDeleted = true;
            this.Update(Student);
        }
    }
}
