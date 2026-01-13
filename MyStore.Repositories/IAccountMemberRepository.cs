using MyStore.Business.Entities;

namespace MyStore.Repositories
{
    public interface IAccountMemberRepository : IRepository<AccountMember>
    {
        AccountMember? GetByEmail(string email);
        AccountMember? Authenticate(string email, string password);
    }
}
