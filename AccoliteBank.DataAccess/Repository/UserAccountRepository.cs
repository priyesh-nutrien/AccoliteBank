using AccoliteBank.DataAccess.Data;
using AccoliteBank.DataAccess.IRepository;
using AccoliteBank.Models;

namespace AccoliteBank.DataAccess.Repository
{
    public class UserAccountRepository : RepositoryAsync<UserAccount>, IUserAccountRepository
    {
        private readonly ApplicationDbContext _db;
        public UserAccountRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }
        public async Task<UserAccount> UpdateAsync(UserAccount userAccount)
        {
            if (userAccount == null)
                return null;
            var exist = await _db.Set<UserAccount>().FindAsync(userAccount.Id);
            if (exist != null)
            {
                _db.Entry(exist).CurrentValues.SetValues(userAccount);
                await _db.SaveChangesAsync();
            }
            return exist;
        }
    }
}
