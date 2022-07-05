using System.Security.Claims;

namespace ElearningDemoServices.IServices;

public interface ILoginService : IBaseService
{
    /// <summary>
    ///   Manager 的登入驗證.
    ///     登入成功：回傳空字串.
    ///     登入失敗：回傳失敗訊息.
    /// </summary>
    /// <param name="username"></param>
    /// <param name="password"></param>
    /// <returns></returns>
    void ManagerLoginCheck(string username, string password, out string? validateStr, out int managerId);
    /// <summary>
    ///   Manager 登入 (claims-based authentication).
    ///   將 manager Id 存入 claims 然後再將 claimsPrincipal 存入 cookies.
    ///   AuthenticationSchema 為 "Admin_Schema" 在 Program.cs 的 service 可設定
    /// </summary>
    /// <param name="managerId"></param>
    /// <returns></returns>
    Task ManagerSignInAsync(int managerId);
    Task SignOutAsync();
    /// <summary>
    ///   Member 的登入驗證.
    ///     登入成功：回傳空字串.
    ///     登入失敗：回傳失敗訊息.
    /// </summary>
    /// <param name="username"></param>
    /// <param name="password"></param>
    /// <returns></returns>
    string MemberLoginCheck(string username, string password);
}
