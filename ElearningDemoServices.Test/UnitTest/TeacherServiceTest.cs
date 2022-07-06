namespace ElearningDemoServices.Test.UnitTest;

public class TeacherServiceTest
{
    private readonly IFakeInstanceGenerator _fakeInstanceGenerator;

    public TeacherServiceTest()
    {
        _fakeInstanceGenerator = new FakeInstanceGenerator();
    }

    [Test]
    public async Task GetTeacherByIdAsync_SingleTeacherId_ReturnCorrectTeacher()
    {
        // Arrange
        var teacherId = 1;
        var fakeTeacher = new Teacher { Id = 1, Name = "test" };

        var _fakeMapper = _fakeInstanceGenerator.FakeMapper();
        var _fakeTeacherRepository = _fakeInstanceGenerator.FakeTeacherRepository();
        var _teacherService = CreateNewTeacherService(_fakeMapper, _fakeTeacherRepository);

        _fakeTeacherRepository.GetOneAsync(Arg.Any<Expression<Func<Teacher, bool>>>()).Returns(fakeTeacher);

        // Act
        var actualTeacherDTO = await _teacherService.GetTeacherByIdAsync(teacherId);

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(actualTeacherDTO, Is.Not.Null);
            Assert.That(actualTeacherDTO.Id, Is.EqualTo(teacherId));
        });
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

        var _fakeMapper = _fakeInstanceGenerator.FakeMapper();
        var _fakeTeacherRepository = _fakeInstanceGenerator.FakeTeacherRepository();
        var _teacherService = CreateNewTeacherService(_fakeMapper, _fakeTeacherRepository);

        _fakeTeacherRepository.GetAllValidAsync().Returns(fakeTeachers.Where(m => m.IsValid).Where(m => !m.IsDeleted));

        // Act
        var actualTeacherDTOs = await _teacherService.GetAllValidTeachersAsync();
        
        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(actualTeacherDTOs, Has.Count.EqualTo(1));
            Assert.That(actualTeacherDTOs.Any(m => m.IsValid == false), Is.False);
            Assert.That(actualTeacherDTOs.Any(m => m.IsDeleted == true), Is.False);
        });
    }

    private static TeacherService CreateNewTeacherService(IMapper mapper, ITeacherRepository teacherRepository)
    {
        return new TeacherService(mapper, teacherRepository);
    }
}
