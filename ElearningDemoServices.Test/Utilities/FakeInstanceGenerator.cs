using Microsoft.AspNetCore.Http;

namespace ElearningDemoServices.Test.Utilities;

public class FakeInstanceGenerator : IFakeInstanceGenerator
{
    public IClassroomRepository FakeClassroomRepository()
    {
        return Substitute.For<IClassroomRepository>();
    }

    public IHttpContextAccessor FakeHttpContextAccessor()
    {
        return Substitute.For<IHttpContextAccessor>();
    }

    public IManagerRepository FakeManagerRepository()
    {
        return Substitute.For<IManagerRepository>();
    }

    public IMapper FakeMapper()
    {
        var mappingConfig = new MapperConfiguration(mc =>
        {
            mc.AddProfile(new AutoMapping());
        });

        IMapper mapper = mappingConfig.CreateMapper();

        return mapper;
    }

    public IMemberRepository FakeMemberRepository()
    {
        return Substitute.For<IMemberRepository>();
    }

    public IQuizRepository FakeQuizRepository()
    {
        return Substitute.For<IQuizRepository>();
    }

    public IStudentRepository FakeStudentRepository()
    {
        return Substitute.For<IStudentRepository>();
    }

    public ITeacherRepository FakeTeacherRepository()
    {
        return Substitute.For<ITeacherRepository>();
    }
}
