using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ElearningDemo.Areas.Admin.ViewModels;

namespace ElearningDemo.Areas.Admin.Controllers;
[AllowAnonymous]
public class LoginController : BaseController
{
    private readonly ILoginService _loginService;
    private readonly IManagerService _managerService;
    public LoginController(ILogger<LoginController> logger, IConfiguration config, IMapper mapper, ILoginService loginService, IManagerService managerService) : base(logger, config, mapper)
    {
        _loginService = loginService;
        _managerService = managerService;
    }

    [Route("admin/login")]
    [HttpGet]
    public IActionResult Login() => View();

    [Route("admin/login")]
    [HttpPost, ActionName(nameof(Login))]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> LoginConfirm(LoginViewModel requestVM)
    {
        if (!ModelState.IsValid) { return View(requestVM); }

        try
        {
            _loginService.ManagerLoginCheck(requestVM.Username, requestVM.Password, out string? validateStr, out int managerId);
            ViewData["ValidateMsg"] = validateStr;

            if (validateStr != null) { return View(); }

            await _loginService.ManagerSignInAsync(managerId);

            return RedirectToAction("Index", "Home", new { area = "Admin" });
        }
        catch (Exception ex)
        {
            _logger.LogError("Error !!", ex);
            return RedirectToAction("Index", "Error");
        }
    }

    [Route("admin/logout")]
    [HttpPost]
    public async Task<IActionResult> Logout()
    {
        await _loginService.SignOutAsync();
        return RedirectToAction("Login");
    }
}

