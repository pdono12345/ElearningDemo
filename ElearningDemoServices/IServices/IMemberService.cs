namespace ElearningDemoServices.IServices;

public interface IMemberService : IBaseService
{
    Task<MemberDTO?> GetMemberByIdAsync(int memberId);
}