namespace ElearningDemoServices.Services;

public class BaseService : IBaseService
{
    protected IMapper _mapper;

    public BaseService(IMapper mapper)
    {
        _mapper = mapper;
    }
}
