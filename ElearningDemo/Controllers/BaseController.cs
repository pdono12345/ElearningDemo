using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ElearningDemo.Controllers;
[Authorize(AuthenticationSchemes = "User_Schema")]
public class BaseController : Controller
{
    protected readonly ILogger _logger;
    protected readonly IConfiguration _config;
    protected readonly IMapper _mapper;

    public BaseController(ILogger logger, IConfiguration config, IMapper mapper)
    {
        _logger = logger;
        _config = config;
        _mapper = mapper;
    }

}

