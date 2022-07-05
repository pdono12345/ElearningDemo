namespace ElearningDemoServices;

public class AutoMapping : Profile
{
    public AutoMapping()
    {
        CreateMap<Manager, ManagerDTO>().ReverseMap();
        CreateMap<Member, MemberDTO>().ReverseMap();
        CreateMap<Student, StudentDTO>().ReverseMap();
        CreateMap<Teacher, TeacherDTO>().ReverseMap();
        CreateMap<Classroom, ClassroomDTO>().ReverseMap();
        CreateMap<Quiz, QuizDTO>().ReverseMap();
    }
}
