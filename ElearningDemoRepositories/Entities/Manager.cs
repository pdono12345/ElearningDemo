namespace ElearningDemoRepositories.Entities;

public class Manager
{
    public int Id { get; set; }
    public string? Name { get; set; }
    public string? Username { get; set; }
    public string? Password { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }
    public bool IsValid { get; set; }
    public bool IsDeleted { get; set; }
}
