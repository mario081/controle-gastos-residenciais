using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Expense.Api.Data;
using Expense.Api.Models;
using Expense.Api.Dtos;

namespace Expense.Api.Controllers
{
    // Criar e listar transações. Menor de 18 só pode cadastrar despesa.
    [ApiController]
    [Route("api/[controller]")]
    public class TransactionsController : ControllerBase
    {
        private readonly AppDbContext _context;

        public TransactionsController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<TransactionResponseDto>>> GetAll()
        {
            var transactions = await _context.Transactions
                .Include(t => t.Person)
                .Select(t => new TransactionResponseDto
                {
                    Id = t.Id,
                    Description = t.Description,
                    Amount = t.Amount,
                    Type = t.Type,
                    PersonId = t.PersonId,
                    PersonName = t.Person != null ? t.Person.Name : string.Empty
                })
                .ToListAsync();

            return Ok(transactions);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<TransactionResponseDto>> GetById(int id)
        {
            var transaction = await _context.Transactions
                .Include(t => t.Person)
                .FirstOrDefaultAsync(t => t.Id == id);

            if (transaction is null)
                return NotFound();

            return Ok(new TransactionResponseDto
            {
                Id = transaction.Id,
                Description = transaction.Description,
                Amount = transaction.Amount,
                Type = transaction.Type,
                PersonId = transaction.PersonId,
                PersonName = transaction.Person?.Name ?? string.Empty
            });
        }

        [HttpPost]
        public async Task<ActionResult<TransactionResponseDto>> Create(CreateTransactionDto dto)
        {
            var person = await _context.People.FindAsync(dto.PersonId);
            if (person is null)
                return BadRequest(new { errors = new[] { "Person not found" } });

            // Requisito: menor de idade só pode cadastrar despesa.
            if (person.Age < 18 && dto.Type == TransactionType.Income)
                return BadRequest(new { errors = new[] { "Income is not allowed for minors" } });

            var transaction = new Transaction
            {
                Description = dto.Description,
                Amount = dto.Amount,
                Type = dto.Type,
                PersonId = dto.PersonId
            };

            _context.Transactions.Add(transaction);
            await _context.SaveChangesAsync();

            var response = new TransactionResponseDto
            {
                Id = transaction.Id,
                Description = transaction.Description,
                Amount = transaction.Amount,
                Type = transaction.Type,
                PersonId = transaction.PersonId,
                PersonName = person.Name
            };

            return CreatedAtAction(nameof(GetById), new { id = transaction.Id }, response);
        }
    }
}
