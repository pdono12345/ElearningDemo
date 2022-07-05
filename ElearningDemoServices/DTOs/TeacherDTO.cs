namespace ElearningDemoServices.DTOs;

public class TeacherDTO
{
    public int Id { get; set; }
    public string Name { get; set; } = null!;
    public DateTime CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }
    public bool IsValid { get; set; }
    public bool IsDeleted { get; set; }
    public int MemberId { get; set; }

    public MemberDTO? Member { get; set; }
}
