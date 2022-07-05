using System.Linq.Expressions;

namespace ElearningDemoServices.Test.UnitTest;

public class TeacherServiceTest
{
    private ITeacherRepository _fakeRepository;
    private IMapper _mapper;
    private TeacherService _target;

    [SetUp]
    public void Setup()
    {
        var mappingConfig = new MapperConfiguration(mc =>
        {
            mc.AddProfile(new AutoMapping());
        });
        IMapper mapper = mappingConfig.CreateMapper();

        _fakeRepository = Substitute.For<ITeacherRepository>();
        _mapper = mapper;
        _target = new TeacherService(_mapper, _fakeRepository);
    }

    [Test]
    public async Task GetTeacherByIdAsync_SingleTeacherId_ReturnCorrectTeacher()
    {
        // Arrange
        var teacherId = 1;
        var fakeTeacher = new Teacher { Id = 1, Name = "test" };
        _fakeRepository.GetOneAsync(Arg.Any<Expression<Func<Teacher, bool>>>()).Returns(Task.FromResult(fakeTeacher));

        // Act
        var actualTeacherDTO = await _target.GetTeacherByIdAsync(teacherId);

        // Assert
        Assert.That(actualTeacherDTO, Is.Not.Null);
        Assert.That(actualTeacherDTO.Id, Is.EqualTo(teacherId));
    }

    [Test]
    public async Task GetAllValidTeachersAsync_Empty_ReturnAllValidTeacher()
    {
        // Arrange
        IEnumerable<Teacher> fakeTeachers = new List<Teacher>() 
        {
            new Teacher { IsValid = true, IsDeleted = false },
            new Teacher { IsValid = true, IsDeleted = true },
            new Teacher { IsValid = false, IsDeleted = false },
        };
        _fakeRepository.GetAllValidAsync().Returns(Task.FromResult(fakeTeachers.Where(m => m.IsValid).Where(m => !m.IsDeleted)));

        // Act
        var actualTeacherDTOs = await _target.GetAllValidTeachersAsync();

        // Assert
        Assert.That(actualTeacherDTOs.Count(), Is.EqualTo(1));
        Assert.That(actualTeacherDTOs.Any(m => m.IsValid == false), Is.False);
        Assert.That(actualTeacherDTOs.Any(m => m.IsDeleted == true), Is.False);
    }


}
