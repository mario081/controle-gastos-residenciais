using System.ComponentModel.DataAnnotations;

namespace expense.api.Dtos{

    public class CreatePersonDto{
        // Se o campo vier vazio, o erro será exibido
        [Required(ErrorMessage = "Name is required")]
        public string Name { get; set;} = string.Empty;

        // Se o campo vier vazio, o erro será exibido
        [Required(ErrorMessage = "Age is required")]
        // Se o campo vier fora do range, o erro será exibido
        [Range(1, 100, ErrorMessage = "Age must be between one and hundred")]
        public int Age { get; set; }
    }
}