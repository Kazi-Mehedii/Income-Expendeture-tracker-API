using DataAccess;
using DataAccess.Entity;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.Text.RegularExpressions;


namespace IncomeExpense_TrackerAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [EnableCors("AllowAllOrigins")]
    public class TransactionController: ControllerBase
    {
        public TransactionController(Context context)
        {
            Context = context;

        }

        public Context Context { get; set; }

        [HttpPost("Upsert")]

        public async Task <IActionResult> Upsert(Transaction transaction)
        {
            transaction.CreatedOn = DateTime.Now;
            Context.Transactions.Update(transaction);
            await Context.SaveChangesAsync();

            return Ok(transaction);
        }

        [HttpGet("GetAll")]

        public async Task<IActionResult> GetAll()
        {
            var transactions = await Context.Transactions.OrderByDescending(t => t.Id).ToListAsync();
            return Ok(transactions);
        }

        [HttpDelete("Delete/{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            if(Context.Transactions.Any(t => t.Id == id))
            {
                var transaction = Context.Transactions.Single(t  => t.Id == id);
                Context.Transactions.Remove(transaction);
                await Context.SaveChangesAsync();
                return Ok("Item Deleted");
            }

            return Ok("Not Deleted");
        }

        [HttpGet("GetMonthlyTransactions")]
        public async Task<IActionResult> GetMothlyTransactions()
        {
            var transactions = await Context.Transactions.ToListAsync();

            var monthlyTransactions =
                transactions
                .GroupBy(k => new { k.Date.Year, k.Date.Month },
                         e => e,
                         (key, group) => new { key.Year, Month = key.Month - 1, Transactions = group.ToList() })
                .OrderByDescending(e => e.Year)
                .ThenByDescending(e => e.Month)
                .ToList();

            return Ok(monthlyTransactions);
        }

    }
}
