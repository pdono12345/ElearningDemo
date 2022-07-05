using ElearningDemo.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ElearningDemo.Controllers;
[AllowAnonymous]
public class HomeController : BaseController
{
    private readonly ITeacherService _teacherService;

    public HomeController(ILogger<HomeController> logger, IConfiguration config, IMapper mapper, ITeacherService teacherService) : base(logger, config, mapper)
    {
        _teacherService = teacherService;
    }

    public IActionResult Index()
    {
        return View();
    }
    public async Task<IActionResult> Teachers()
    {
        var validTeachers = _mapper.Map<List<HomeTeacherViewModel>>(await _teacherService.GetAllValidTeachersAsync());

        return View(validTeachers);
    }
}
