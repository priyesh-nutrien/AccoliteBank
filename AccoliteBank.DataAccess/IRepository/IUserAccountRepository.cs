using AccoliteBank.Models;

namespace AccoliteBank.DataAccess.IRepository
{
    public interface IUserAccountRepository : IRepositoryAsync<UserAccount>
    {
        public Task<UserAccount> UpdateAsync(UserAccount userAccount);
    }
}
