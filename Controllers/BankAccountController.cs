using BankApi.Context;
using BankApi.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BankApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BankAccountController : ControllerBase
    {
        private readonly AppDbContext _dbContext;

        public BankAccountController(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        [HttpGet]
        public ActionResult<IEnumerable <BankAccountModel>> Get()
        {
            var allAccounts = _dbContext.BankAccount.AsNoTracking().ToList();
            
            return Ok(allAccounts);
        }

        [HttpGet("{document}")]
        public ActionResult<BankAccountModel> Get(string document)
        {
            try
            {
                var accountByDocument = _dbContext.BankAccount.AsNoTracking().FirstOrDefault(b => b.DocumentClient == document);

                if (accountByDocument == null)
                {
                    return NotFound("A conta não existe");
                }
                return Ok(accountByDocument);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        [HttpPost]
        public ActionResult Post(BankAccountModel bankAccount)
        {
            try
            {
                if (_dbContext.BankAccount.Any(b => b.EmailClient == bankAccount.EmailClient
                    || b.DocumentClient == bankAccount.DocumentClient
                    || b.PhoneClient == bankAccount.PhoneClient))
                {
                    ModelState.AddModelError("EmailClient", "Esse email já está em uso.");
                    ModelState.AddModelError("DocumentClient", "Esse documento já está em uso.");
                    ModelState.AddModelError("PhoneClient", "Esse número de telefone já está em uso.");
                    return BadRequest(ModelState);
                }

                _dbContext.BankAccount.Add(bankAccount);
                _dbContext.SaveChanges();

                return new CreatedAtRouteResult(new { id = bankAccount.AccountId }, "Conta criada com sucesso");
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        [HttpPut("{document}")]
        public ActionResult Put(string document, BankAccountModel updatedAccount)
        {
            try
            {
                var existingAccount = _dbContext.BankAccount.FirstOrDefault(b => b.DocumentClient == document);

                if (existingAccount == null)
                {
                    return NotFound("A conta não existe");
                }

                existingAccount.NameClient = updatedAccount.NameClient;
                existingAccount.EmailClient = updatedAccount.EmailClient;
                existingAccount.PhoneClient = updatedAccount.PhoneClient;

                _dbContext.Entry(existingAccount).State = EntityState.Modified;

                _dbContext.SaveChanges();

                return Ok(existingAccount);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        [HttpDelete("{document}")]
        public ActionResult<BankAccountModel> Delete(string document)
        {
            try
            {
                var accountByDocument = _dbContext.BankAccount.AsNoTracking().FirstOrDefault(b => b.DocumentClient == document);

                if (accountByDocument == null)
                {
                    return NotFound("A conta não existe");
                }

                if (accountByDocument.BalanceClient == 0)
                {
                    _dbContext.BankAccount.Remove(accountByDocument);
                    _dbContext.SaveChanges();
                    return Ok("Conta deletada com sucesso");
                }
                else
                {
                    return BadRequest("Não é possível excluir a conta, pois o saldo não é zero.");
                }
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }
    }
}
