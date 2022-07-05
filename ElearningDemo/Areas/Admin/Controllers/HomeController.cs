using Microsoft.AspNetCore.Mvc;

namespace ElearningDemo.Areas.Admin.Controllers;
public class HomeController : BaseController
{
    public HomeController(ILogger<HomeController> logger, IConfiguration config, IMapper mapper) : base(logger, config, mapper)
    {
    }

    public IActionResult Index()
    {
        return View();
    }
}

