using AccoliteBank.Models;
using AccoliteBank.Services.IServices;
using Microsoft.AspNetCore.Mvc;

namespace AccoliteBank.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UserController : ControllerBase
    {
        private readonly IUserAccountService _userAccountService;
        public UserController(IUserAccountService userAccountService)
        {
            _userAccountService = userAccountService;
        }

        [HttpGet("{id}/accounts")]
        public async Task<IEnumerable<UserAccount>> GetAccounts(int id)
        {
            var list = await _userAccountService.GetAllAccountByUser(id);
            return list;
        }

        [HttpPost("{id}/accounts")]
        public async Task<ObjectResult> CreateAccount(int id, [FromBody] CreateUserAccount createAccount)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var result = await _userAccountService.CreateAccount(new UserAccount()
                    {
                        UserId = id,
                        AccountTypeId = createAccount.AccountTypeId,
                        Balance = createAccount.Balance,
                        CreatedDate = DateTime.Now,
                        Name = createAccount.Name
                    });
                    return Ok(result);
                }
                else
                    return StatusCode(500, "Invalid data!");
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
        [HttpDelete("{id}/accounts/{accountId}")]
        public async Task<ObjectResult> DeleteAccount(int id, int accountId)
        {
            try
            {
                var result = await _userAccountService.DeleteAccount(id, accountId);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPut("{id}/accounts/{accountId}/deposit")]
        public async Task<ObjectResult> Deposit(int id, int accountId, [FromBody] Transaction transaction)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var result = await _userAccountService.Deposit(id, accountId, transaction.Amount);
                    return Ok(result);
                }
                else
                {
                    string errorMessages = string.Join(Environment.NewLine, ModelState.Values
                                                                .SelectMany(v => v.Errors)
                                                                .Select(e => e.ErrorMessage));
                    return StatusCode(500, errorMessages);
                }

            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
        [HttpPut("{id}/accounts/{accountId}/withdraw")]
        public async Task<ObjectResult> Withdraw(int id, int accountId, [FromBody] Transaction transaction)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var result = await _userAccountService.Withdraw(id, accountId, transaction.Amount);
                    return Ok(result);
                }
                else
                {
                    string errorMessages = string.Join(Environment.NewLine, ModelState.Values
                                                                .SelectMany(v => v.Errors)
                                                                .Select(e => e.ErrorMessage));
                    return StatusCode(500, errorMessages);
                }

            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
    }
}