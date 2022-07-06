using Microsoft.AspNetCore.Http;

namespace ElearningDemoServices.Test.UnitTest;

public class LoginServiceTest
{
    private readonly IFakeInstanceGenerator _fakeInstanceGenerator;

    public LoginServiceTest()
    {
        _fakeInstanceGenerator = new FakeInstanceGenerator();
    }

    [TestCase("username", "password")]
    public void ManagerLoginCheck_CanNotFindManager_OutValidateStrAndOutManagerIdEquals0(string username, string password)
    {
        // Arrange
        var loginFailManagerId = 0;

        var _fakeMapper = FakeMapper();
        var _fakeManagerRepository = _fakeInstanceGenerator.FakeManagerRepository();
        var _fakeHttpContextAccessor = _fakeInstanceGenerator.FakeHttpContextAccessor();
        var _loginService = CreateNewLoginService(_fakeMapper, _fakeManagerRepository, _fakeHttpContextAccessor);

        _fakeManagerRepository.GetOne(Arg.Any<Expression<Func<Manager, bool>>>());

        // Act
        _loginService.ManagerLoginCheck(username, password, out string? validateStr, out int managerId);
        
        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(validateStr, Is.Not.Null);
            Assert.That(loginFailManagerId, Is.EqualTo(managerId));
        });
    }

    [TestCase("admin", "wrongPwd")]
    [TestCase("admin", "Admin")]
    public void ManagerLoginCheck_WrongPwd_OutValidateStr(string username, string password)
    {
        // Arrange
        var fakeManager = new Manager { Id = 1, Username = "admin", Password = "admin" };
        var loginFailManagerId = 0;

        var _fakeMapper = FakeMapper();
        var _fakeManagerRepository = _fakeInstanceGenerator.FakeManagerRepository();
        var _fakeHttpContextAccessor = _fakeInstanceGenerator.FakeHttpContextAccessor();
        var _loginService = CreateNewLoginService(_fakeMapper, _fakeManagerRepository, _fakeHttpContextAccessor);

        _fakeManagerRepository.GetOne(Arg.Any<Expression<Func<Manager, bool>>>()).Returns(fakeManager);
        // Act
        _loginService.ManagerLoginCheck(username, password, out string? validateStr, out int managerId);
        
        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(validateStr, Is.Not.Null);
            Assert.That(loginFailManagerId, Is.EqualTo(managerId));
        });
    }

    [Test]
    public void ManagerLoginCheck_CorrectUsernameAndPwd_OutCorrectManagerIdAndNullValidateStr()
    {
        // Arrange
        var username = "admin";
        var password = "admin";
        var fakeManager = new Manager { Id = 1, Name = "admin", Username = "admin", Password = "admin", CreatedAt = DateTime.Now, UpdatedAt = null, IsValid = true, IsDeleted = false };
        var _fakeMapper = FakeMapper();
        var _fakeManagerRepository = _fakeInstanceGenerator.FakeManagerRepository();
        var _fakeHttpContextAccessor = _fakeInstanceGenerator.FakeHttpContextAccessor();
        var _loginService = CreateNewLoginService(_fakeMapper, _fakeManagerRepository, _fakeHttpContextAccessor);

        _fakeManagerRepository.GetOne(Arg.Any<Expression<Func<Manager, bool>>>()).Returns(fakeManager);

        // Act
        _loginService.ManagerLoginCheck(username, password, out string? validateStr, out int managerId);
        
        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(validateStr, Is.Null);
            Assert.That(managerId, Is.EqualTo(1));
        });
    }
    private static IMapper FakeMapper() 
    {
        var mappingConfig = new MapperConfiguration(mc =>
        {
            mc.AddProfile(new AutoMapping());
        });

        IMapper mapper = mappingConfig.CreateMapper();

        return mapper;
    }
    private static LoginService CreateNewLoginService(IMapper mapper, IManagerRepository managerRepository, IHttpContextAccessor httpContextAccessor) 
    {
        return new LoginService(mapper, managerRepository, httpContextAccessor);
    }
}
