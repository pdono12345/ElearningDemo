using ElearningDemoRepositories.Entities;
using Microsoft.EntityFrameworkCore;

namespace ElearningDemoRepositories.Data
{
    public class ElearningDemoContext : DbContext
    {
        public ElearningDemoContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Classroom> Classrooms { get; set; }
        public DbSet<Manager> Managers { get; set; }
        public DbSet<Member> Members { get; set; }
        public DbSet<Quiz> Quizzes { get; set; }
        public DbSet<Student> Students { get; set; }
        public DbSet<Teacher> Teachers { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Classroom>().ToTable(nameof(Classroom));
            modelBuilder.Entity<Manager>().ToTable(nameof(Manager));
            modelBuilder.Entity<Member>().ToTable(nameof(Member));
            modelBuilder.Entity<Quiz>().ToTable(nameof(Quiz));
            modelBuilder.Entity<Student>().ToTable(nameof(Student));
            modelBuilder.Entity<Teacher>().ToTable(nameof(Teacher));
        }
    }
}
