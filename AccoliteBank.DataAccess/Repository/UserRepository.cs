using AccoliteBank.DataAccess.Data;
using AccoliteBank.DataAccess.IRepository;
using AccoliteBank.Models;

namespace AccoliteBank.DataAccess.Repository
{
    public class UserRepository : RepositoryAsync<User>, IUserRepository
    {
        private readonly ApplicationDbContext _db;
        public UserRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }
    }
}
