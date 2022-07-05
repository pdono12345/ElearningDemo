namespace ElearningDemoRepositories.Entities;

public class Classroom
{
    public int Id { get; set; }
    public string Name { get; set; } = null!;
    public DateTime CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }
    public bool IsValid { get; set; }
    public bool IsDeleted { get; set; }
    public int TeacherId { get; set; }

    public Teacher? Teacher { get; set; }
    public ICollection<Student>? Students { get; set; }
}
