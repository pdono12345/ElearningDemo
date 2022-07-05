namespace ElearningDemoServices.IServices;

public interface ITeacherService : IBaseService
{
    Task<TeacherDTO> GetTeacherByIdAsync(int teacherId);
    Task<List<TeacherDTO>> GetAllValidTeachersAsync();
}