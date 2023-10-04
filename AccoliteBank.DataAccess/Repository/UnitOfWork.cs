using AccoliteBank.DataAccess.Data;
using AccoliteBank.DataAccess.IRepository;

namespace AccoliteBank.DataAccess.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext _db;
        public UnitOfWork(ApplicationDbContext db)
        {
            _db = db;
            UserAccount = new UserAccountRepository(_db);
            User = new UserRepository(_db);
        }

        public IUserRepository User { get; private set; }
        public IUserAccountRepository UserAccount{ get; private set; }

        public async ValueTask DisposeAsync()
        {
            await _db.DisposeAsync();
        }
        public async Task SaveChangesAsync()
        {
            await _db.SaveChangesAsync();
        }
    }
}
