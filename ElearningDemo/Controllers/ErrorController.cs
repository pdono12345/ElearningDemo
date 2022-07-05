using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ElearningDemo.Controllers;

[AllowAnonymous]
public class ErrorController : BaseController
{
    public ErrorController(ILogger<ErrorController> logger, IConfiguration config, IMapper mapper) : base(logger, config, mapper)
    {
    }

    public IActionResult Index()
    {
        return View();
    }
}
