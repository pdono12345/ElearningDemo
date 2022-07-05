namespace ElearningDemoServices.DTOs;

public class ClassroomDTO
{
    public int Id { get; set; }
    public string Name { get; set; } = null!;
    public DateTime CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }
    public bool IsValid { get; set; }
    public bool IsDeleted { get; set; }
    public int TeacherId { get; set; }

    public TeacherDTO? Teacher { get; set; }
    public ICollection<StudentDTO>? Students { get; set; }
}
