using Microsoft.AspNetCore.Http;
using System.Linq.Expressions;

namespace ElearningDemoServices.Test.UnitTest;

public class LoginServiceTest
{
    private IManagerRepository _fakeRepository;
    private IHttpContextAccessor _httpContextAccessor;
    private IMapper _mapper;
    private LoginService _target;

    [SetUp]
    public void Setup()
    {
        var mappingConfig = new MapperConfiguration(mc =>
        {
            mc.AddProfile(new AutoMapping());
        });
        IMapper mapper = mappingConfig.CreateMapper();

        _fakeRepository = Substitute.For<IManagerRepository>();
        _httpContextAccessor = Substitute.For<HttpContextAccessor>();
        _mapper = mapper;
        _target = new LoginService(_mapper, _fakeRepository, _httpContextAccessor);
    }

    [Test]
    public void ManagerLoginCheck_WrongUsername_OutValidateStrAndManagerIdwhichEquals0()
    {
        // Arrange
        var username = "wrongUsername";
        var password = "admin";
        _fakeRepository.GetOne(Arg.Any<Expression<Func<Manager, bool>>>());

        // Act
        _target.ManagerLoginCheck(username, password, out string validateStr, out int managerId);

        // Assert
        Assert.That(validateStr, Is.Not.Null);
        Assert.That(managerId, Is.EqualTo(0));
    }
    [Test]
    public void ManagerLoginCheck_WrongPwd_OutValidateStrAndManagerIdwhichEquals0()
    {
        // Arrange
        var username = "admin";
        var password = "wrongPwd";
        var fakeManager = new Manager { Id = 1, Username = "admin", Password = "admin" };
        _fakeRepository.GetOne(Arg.Any<Expression<Func<Manager, bool>>>()).Returns(fakeManager);

        // Act
        _target.ManagerLoginCheck(username, password, out string validateStr, out int managerId);

        // Assert
        Assert.That(validateStr, Is.Not.Null);
        Assert.That(managerId, Is.EqualTo(0));
    }
    [Test]
    public void ManagerLoginCheck_CorrectUsernameAndPwd_OutCorrectManagerIdAndNullValidateStr()
    {
        // Arrange
        var username = "admin";
        var password = "admin";
        var fakeManager = new Manager { Id = 1, Username = "admin", Password = "admin" };
        _fakeRepository.GetOne(Arg.Any<Expression<Func<Manager, bool>>>()).Returns(fakeManager);

        // Act
        _target.ManagerLoginCheck(username, password, out string validateStr, out int managerId);

        // Assert
        Assert.That(validateStr, Is.Null);
        Assert.That(managerId, Is.EqualTo(1));
    }
}
