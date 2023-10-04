using AccoliteBank.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccoliteBank.Services.IServices
{
    public interface IUserAccountService
    {
        Task<UserAccount?> Get(int id);
        Task<List<UserAccount>> GetAllAccountByUser(int userId);
        Task<UserAccount> CreateAccount(UserAccount userAccount);
        Task<Response> DeleteAccount(int userId, int id);
        Task<decimal> GetBalance(int userId,int accountId);
        Task<decimal> Deposit(int userId, int accountId, decimal amount);
        Task<decimal> Withdraw(int userId, int accountId, decimal amount);
    }
}
