namespace ElearningDemoServices.IServices;

public interface IManagerService : IBaseService
{
    Task<ManagerDTO> GetManagerByIdAsync(int managerId);
}
