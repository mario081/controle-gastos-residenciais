using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Expense.Api.Data;
using Expense.Api.Models;
using Expense.Api.Dtos;

namespace Expense.Api.Controllers
{
    // Totais por pessoa + totais gerais (receita, despesa e saldo).
    [ApiController]
    [Route("api/[controller]")]
    public class SummaryController : ControllerBase
    {
        private readonly AppDbContext _context;

        public SummaryController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<SummaryResponseDto>> GetSummary()
        {
            var people = await _context.People
                .Include(p => p.Transactions)
                .ToListAsync();

            var summaries = people.Select(p =>
            {
                var income = p.Transactions
                    .Where(t => t.Type == TransactionType.Income)
                    .Sum(t => t.Amount);
                var expense = p.Transactions
                    .Where(t => t.Type == TransactionType.Expense)
                    .Sum(t => t.Amount);

                return new PersonSummaryDto
                {
                    PersonId = p.Id,
                    PersonName = p.Name,
                    TotalIncome = income,
                    TotalExpense = expense,
                    Balance = income - expense
                };
            }).ToList();

            var response = new SummaryResponseDto
            {
                People = summaries,
                TotalIncome = summaries.Sum(s => s.TotalIncome),
                TotalExpense = summaries.Sum(s => s.TotalExpense),
                Balance = summaries.Sum(s => s.Balance)
            };

            return Ok(response);
        }
    }
}
