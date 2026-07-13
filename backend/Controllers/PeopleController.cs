using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Expense.Api.Data;
using Expense.Api.Models;
using Expense.Api.Dtos;

namespace Expense.Api.Controllers{

    // Habilita a API a ser acessada por outras aplicações
    [ApiController]

    // Rota base para acessar o controlador
    [Route("api/[controller]")]
    public class PeopleController : ControllerBase{
        private readonly AppDbContext _context;

        // Injeção de dependência do contexto do banco de dados
        public PeopleController(AppDbContext context){
            _context = context;
        }


        // Rota para da o Get por ID
        [HttpGet("{id}")]
        public async Task<ActionResult<PersonResponseDto>> GetById([FromRoute] int id){
            var person = await _context.People.FindAsync(id);

            // Se a pessoa não for encontrada, retorna um erro 404
            if (person == null){
                return NotFound();
            }

            // Se a pessoa for encontrada, retorna a pessoa
            var response = new PersonResponseDto{
                Id = person.Id,
                Name = person.Name,
                Age = person.Age
            };

            return Ok(response);
        }

        // Rota para criar um nova pessoa
        [HttpPost]
        public async Task<ActionResult<PersonResponseDto>> Create(CreatePersonDto createPersonDto){
            var person = new Person{
                Name = createPersonDto.Name,
                Age = createPersonDto.Age
            };

            _context.People.Add(person);

            await _context.SaveChangesAsync();

            var response = new PersonResponseDto{
                Id = person.Id,
                Name = person.Name,
                Age = person.Age
            };

            return CreatedAtAction(nameof(GetById), new {
                id = person.Id
            }, response);
        }

        // Rota para deletar uma pessoa
        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete([FromRoute] int id){
            var person = await _context.People.FindAsync(id);

            if (person == null){
                return NotFound();
            }

            _context.People.Remove(person);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}