namespace AccoliteBank.DataAccess.IRepository
{
    public interface IUnitOfWork : IAsyncDisposable
    {
        IUserRepository User { get; }
        IUserAccountRepository UserAccount{ get; }
        Task SaveChangesAsync();
    }
}
