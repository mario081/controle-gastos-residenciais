using System.ComponentModel.DataAnnotations;

namespace Expense.Api.Dtos
{
    // Entrada do POST de pessoa.
    public class CreatePersonDto
    {
        [Required(ErrorMessage = "Name is required")]
        public string Name { get; set; } = string.Empty;

        // Range cobre o caso de Age omitido no JSON (vira 0 e falha a validação).
        [Range(1, 120, ErrorMessage = "Age must be between 1 and 120")]
        public int Age { get; set; }
    }
}
