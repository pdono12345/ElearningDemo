namespace ElearningDemoServices.Services;

public class MemberService : BaseService, IMemberService
{
    public MemberService(IMapper mapper) : base(mapper)
    {
    }

    public Task<MemberDTO> GetMemberByIdAsync(int MemberId)
    {
        throw new NotImplementedException();
    }
}
