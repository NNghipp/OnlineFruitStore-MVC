using MyStore.Business.Entities;
using MyStore.Repositories;

namespace MyStore.Services
{
    public class AccountMemberService : IAccountMemberService
    {
        private readonly IAccountMemberRepository _accountMemberRepository;

        public AccountMemberService(IAccountMemberRepository accountMemberRepository)
        {
            _accountMemberRepository = accountMemberRepository;
        }

        public IEnumerable<AccountMember> GetAllMembers()
        {
            return _accountMemberRepository.GetAll();
        }

        public AccountMember? GetMemberById(int id)
        {
            return _accountMemberRepository.GetById(id);
        }

        public AccountMember? Authenticate(string email, string password)
        {
            return _accountMemberRepository.Authenticate(email, password);
        }

        public void CreateMember(AccountMember member)
        {
            _accountMemberRepository.Add(member);
            _accountMemberRepository.Save();
        }

        public void UpdateMember(AccountMember member)
        {
            _accountMemberRepository.Update(member);
            _accountMemberRepository.Save();
        }

        public void DeleteMember(int id)
        {
            var member = _accountMemberRepository.GetById(id);
            if (member != null)
            {
                _accountMemberRepository.Delete(member);
                _accountMemberRepository.Save();
            }
        }
    }
}
