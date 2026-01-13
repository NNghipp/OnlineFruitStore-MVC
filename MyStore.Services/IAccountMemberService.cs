using MyStore.Business.Entities;

namespace MyStore.Services
{
    public interface IAccountMemberService
    {
        IEnumerable<AccountMember> GetAllMembers();
        AccountMember? GetMemberById(int id);
        AccountMember? Authenticate(string email, string password);
        void CreateMember(AccountMember member);
        void UpdateMember(AccountMember member);
        void DeleteMember(int id);
    }
}
