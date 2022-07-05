namespace ElearningDemoRepositories.Repositories;

public class QuizRepository : BaseRepository<Quiz>, IQuizRepository
{
    public QuizRepository(ElearningDemoContext db) : base(db)
    {
    }

    public override async Task<IEnumerable<Quiz>?> GetAllAliveAsync()
    {
        return await _db.Quizzes
            .Where(m => m.IsDeleted == false)
            .ToListAsync();
    }

    public override async Task<IEnumerable<Quiz>?> GetAllValidAsync()
    {
        return await _db.Quizzes
            .Where(m => m.IsValid == true)
            .Where(m => m.IsDeleted == false)
            .ToListAsync();
    }

    public override void SoftDelete(int id)
    {
        var Quiz = _db.Quizzes.SingleOrDefault(m => m.Id == id);

        if (Quiz != null)
        {
            Quiz.IsDeleted = true;
            this.Update(Quiz);
        }
    }
}
