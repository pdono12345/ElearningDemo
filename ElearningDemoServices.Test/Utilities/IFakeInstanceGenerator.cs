using Microsoft.AspNetCore.Http;

namespace ElearningDemoServices.Test.Utilities;

public interface IFakeInstanceGenerator
{
    IMapper FakeMapper();
    IHttpContextAccessor FakeHttpContextAccessor();
    IManagerRepository FakeManagerRepository();
    ITeacherRepository FakeTeacherRepository();
    IStudentRepository FakeStudentRepository();
    IMemberRepository FakeMemberRepository();
    IClassroomRepository FakeClassroomRepository();
    IQuizRepository FakeQuizRepository();
}
