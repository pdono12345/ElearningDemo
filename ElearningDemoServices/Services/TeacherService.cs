namespace ElearningDemoServices.Services;

public class TeacherService : BaseService, ITeacherService
{
    private readonly ITeacherRepository _teacherRepository;
    public TeacherService(IMapper mapper, ITeacherRepository teacherRepository) : base(mapper)
    {
        _teacherRepository = teacherRepository;
    }

    public async Task<List<TeacherDTO>> GetAllValidTeachersAsync() => _mapper.Map<List<TeacherDTO>>(await _teacherRepository.GetAllValidAsync());

    public async Task<TeacherDTO> GetTeacherByIdAsync(int teacherId) => _mapper.Map<TeacherDTO>(await _teacherRepository.GetOneAsync(m => m.Id == teacherId));
}
