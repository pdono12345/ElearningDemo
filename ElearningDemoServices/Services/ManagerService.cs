namespace ElearningDemoServices.Services;

public class ManagerService : BaseService, IManagerService
{
    public ManagerService(IMapper mapper) : base(mapper)
    {
    }

    public Task<ManagerDTO> GetManagerByIdAsync(int managerId)
    {
        throw new NotImplementedException();
    }
}
