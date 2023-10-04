using AccoliteBank.DataAccess.IRepository;
using AccoliteBank.Models;
using AccoliteBank.Services.IServices;

namespace AccoliteBank.Services
{
    public class UserAccountService : IUserAccountService
    {
        private readonly IUnitOfWork _unitOfWork;
        public UserAccountService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<UserAccount> CreateAccount(UserAccount userAccount)
        {
            var user = await _unitOfWork.User.GetAsync(userAccount.UserId);
            if (user == null)
                throw new MyCustomException("User not exist!");

            if (userAccount.Balance >= 100)
            {
                await _unitOfWork.UserAccount.AddAsync(userAccount);
                await _unitOfWork.SaveChangesAsync();
                return userAccount;
            }
            else
                throw new MyCustomException("An account cannot have less than $100 at any time in an account.");
        }

        public async Task<Response> DeleteAccount(int userId, int accountId)
        {
            var user = await _unitOfWork.User.GetAsync(userId);
            if (user == null)
                throw new MyCustomException("User not exist!");

            var userAccount = await _unitOfWork.UserAccount.GetAsync(accountId);
            if (userAccount == null)
                throw new MyCustomException("Account not exist!");

            await _unitOfWork.UserAccount.RemoveAsync(userAccount);
            await _unitOfWork.SaveChangesAsync();

            return new Response { Success = true, Message = "Deleted Successfully!" };
        }

        public async Task<decimal> Deposit(int userId, int accountId, decimal amount)
        {
            var user = await _unitOfWork.User.GetAsync(userId);
            if (user == null)
                throw new MyCustomException("User not exist!");

            var userAccount = await _unitOfWork.UserAccount.GetAsync(accountId);
            if (userAccount == null)
                throw new MyCustomException("Account not exist!");

            if (amount > 10000)
                throw new MyCustomException("A user cannot deposit more than $10,000 in a single transaction.");

            var balance = userAccount.Balance;
            userAccount.Balance = balance + amount;

            await _unitOfWork.UserAccount.UpdateAsync(userAccount);
            await _unitOfWork.SaveChangesAsync();
            return userAccount.Balance;

        }
        public async Task<decimal> Withdraw(int userId, int accountId, decimal amount)
        {
            var user = await _unitOfWork.User.GetAsync(userId);
            if (user == null)
                throw new MyCustomException("User not exist!");

            var userAccount = await _unitOfWork.UserAccount.GetAsync(accountId);
            if (userAccount == null)
                throw new MyCustomException("Account not exist!");

            var balance = userAccount.Balance - amount;
            if (balance >= 100)
            {
                decimal percentage = (amount / userAccount.Balance) * 100;
                if (percentage > 90)
                    throw new MyCustomException("A user cannot withdraw more than 90% of their total balance from an account in a single transaction.");

                userAccount.Balance = balance;

                await _unitOfWork.UserAccount.UpdateAsync(userAccount);
                await _unitOfWork.SaveChangesAsync();
                return userAccount.Balance;
            }
            else
                throw new MyCustomException("An account cannot have less than $100 at any time in an account.");

        }

        public async Task<UserAccount?> Get(int id)
        {
            var result = await _unitOfWork.UserAccount.GetAsync(id);
            if (result == null)
                return null;

            return result;
        }

        public async Task<List<UserAccount>> GetAllAccountByUser(int userId)
        {
            var result = await _unitOfWork.UserAccount.GetAllAsync(x => x.UserId == userId);
            return result.ToList();
        }

        public async Task<decimal> GetBalance(int userId, int accountId)
        {
            var user = await _unitOfWork.User.GetAsync(userId);
            if (user == null)
                throw new MyCustomException("User not exist!");

            var userAccount = await _unitOfWork.UserAccount.GetAsync(accountId);
            if (userAccount == null)
                throw new MyCustomException("Account not exist!");

            return userAccount.Balance;
        }
    }
}
