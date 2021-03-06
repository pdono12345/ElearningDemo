namespace ElearningDemoRepositories.Entities;

public class Teacher
{
    public int Id { get; set; }
    public string? Name { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }
    public bool IsValid { get; set; }
    public bool IsDeleted { get; set; }
    public int MemberId { get; set; }

    public Member? Member { get; set; }
}
