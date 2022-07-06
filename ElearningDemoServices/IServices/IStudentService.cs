namespace ElearningDemoServices.IServices;

public interface IStudentService : IBaseService
{
    Task<StudentDTO?> GetStudentByIdAsync(int studentId);
}