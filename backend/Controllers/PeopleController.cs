using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Expense.Api.Data;
using Expense.Api.Models;
using Expense.Api.Dtos;

namespace Expense.Api.Controllers
{
    // CRUD de pessoas: criar, buscar, listar e deletar.
    [ApiController]
    [Route("api/[controller]")]
    public class PeopleController : ControllerBase
    {
        private readonly AppDbContext _context;

        public PeopleController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<PersonResponseDto>>> GetAll()
        {
            var people = await _context.People
                .Select(p => new PersonResponseDto
                {
                    Id = p.Id,
                    Name = p.Name,
                    Age = p.Age
                })
                .ToListAsync();

            return Ok(people);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<PersonResponseDto>> GetById(int id)
        {
            var person = await _context.People.FindAsync(id);
            if (person == null)
                return NotFound();

            return Ok(new PersonResponseDto
            {
                Id = person.Id,
                Name = person.Name,
                Age = person.Age
            });
        }

        [HttpPost]
        public async Task<ActionResult<PersonResponseDto>> Create(CreatePersonDto dto)
        {
            var person = new Person
            {
                Name = dto.Name,
                Age = dto.Age
            };

            _context.People.Add(person);
            await _context.SaveChangesAsync();

            var response = new PersonResponseDto
            {
                Id = person.Id,
                Name = person.Name,
                Age = person.Age
            };

            return CreatedAtAction(nameof(GetById), new { id = person.Id }, response);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            var person = await _context.People.FindAsync(id);
            if (person is null)
                return NotFound();

            // Transações da pessoa são removidas automaticamente (Cascade no AppDbContext).
            _context.People.Remove(person);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
