using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Expense.Api.Data;
using Expense.Api.Models;
using Expense.Api.Dtos;

// Controlador de transações
namespace Expense.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class TransactionController : ControllerBase{
    private readonly AppDbContext _context;

    public TransactionController(AppDbContext context){
        _contetxt = context;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<TransactionResponseDto>>> GetAll(){
        var Transactions = await _contetxt.Transactions
        .Include(t => t.Person)
        .select(t => new TransactionController{
            Id = t.Id,
            Description = t.Description,
            Amount = t.Amount,
            Type = t.Type,
            PersonId = t.PersonId,
            PersonName = t.Person.Name
        })

        .toListAsync();

        return Ok(Transactions);
    }

    [HttpPost]
    public async Task<ActionResult<TransactionResponseDto>> Create(CreateTransactionDto dto){
        var person = await _contetxt.People.FindAsync(dto.PersonId);

        if( person == null){
            return BadRequest(new { errors = ["Person not found"] });
        }

        if (person.Age < 18 && dto.Type == TransactionType.Income){
            return BadRequest(new { errors = ["Income is not allowed for minors"] });
        }

        var transaction = new Transaction{
            Description = dto.Description,
            Amount = dto.Amount,
            Type = dto.Type,
            PersonId = dto.PersonId
        }

        _contetxt.Transactions.Add(transaction);
        await _contetxt.SaveChangesAsync();

        var response = new TransactionResponseDto{
            Id = transaction.Id,
            Description = transaction.Description,
            Amount = transaction.Amount,
            Type = transaction.Type,
            PersonId = transaction.PersonId,
            PersonName = person.Name
        }

        return CreatedAtAction(nameof(GetById), new { id = transaction.Id}, response);
    }
}