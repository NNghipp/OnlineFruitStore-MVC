using MyStore.Business;
using MyStore.Business.Entities;

namespace MyStore.Repositories
{
    public class AccountMemberRepository : Repository<AccountMember>, IAccountMemberRepository
    {
        public AccountMemberRepository(MyStoreContext context) : base(context) { }

        public AccountMember? GetByEmail(string email)
        {
            return _dbSet.FirstOrDefault(m => m.EmailAddress == email);
        }

        public AccountMember? Authenticate(string email, string password)
        {
            return _dbSet.FirstOrDefault(m => m.EmailAddress == email && m.MemberPassword == password);
        }
    }
}
