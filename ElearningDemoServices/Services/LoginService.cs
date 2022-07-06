using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;

namespace ElearningDemoServices.Services;

public class LoginService : BaseService, ILoginService
{
    private readonly IManagerRepository _managerRepository;
    private readonly IHttpContextAccessor _httpContextAccessor;
    public LoginService(IMapper mapper, IManagerRepository managerRepository, IHttpContextAccessor httpContext) : base(mapper)
    {
        _managerRepository = managerRepository;
        _httpContextAccessor = httpContext;
    }

    public void ManagerLoginCheck(string username, string password, out string? validateStr, out int managerId)
    {
        managerId = 0;
        validateStr = null;
        var manager = _managerRepository.GetOne(m => m.Username == username && m.IsValid && !m.IsDeleted);

        if (manager == null)
        {
            validateStr = "帳號或密碼輸入錯誤";
            return;
        }

        if (manager.Password != password) 
        {
            validateStr = "帳號或密碼輸入錯誤";
            return;
        }

        managerId = manager.Id;
        return;
    }

    public async Task ManagerSignInAsync(int managerId)
    {
        var principal = CreateClaimsPrincipal(managerId);

        await _httpContextAccessor.HttpContext.SignInAsync("Admin_Schema", principal);
    }
    /// <summary>
    ///     將 managerId 以 ClaimTypes.Sid, managerId.ToString() 存放至 claims
    ///     再將 claims 放入 claimsIdentity
    ///     再將 claimsIdentity 放入 claimsPrincipal 之後回傳
    /// </summary>
    /// <param name="managerId"></param>
    /// <returns></returns>
    private static ClaimsPrincipal CreateClaimsPrincipal(int managerId)
    {
        var claims = new[] { new Claim(ClaimTypes.Sid, managerId.ToString()) };
        var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);

        return new ClaimsPrincipal(claimsIdentity);
    }

    public async Task SignOutAsync()
    {
        await _httpContextAccessor.HttpContext.SignOutAsync();
    }
    public string MemberLoginCheck(string username, string password)
    {
        throw new NotImplementedException();
    }

}
