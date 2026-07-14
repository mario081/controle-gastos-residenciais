using System.ComponentModel.DataAnnotations;
using Expense.Api.Models;

namespace Expense.Api.Dtos
{
    public class CreateTransactionDto
    {
        [Required(ErrorMessage = "The description is required")]
        public string Description { get; set; } = string.Empty;

        [Range(0.01, double.MaxValue, ErrorMessage = "The amount must be greater than zero")]
        public decimal Amount { get; set; }

        [Required(ErrorMessage = "The type is required")]
        public TransactionType Type { get; set; }

        [Range(1, int.MaxValue, ErrorMessage = "The person ID is required")]
        public int PersonId { get; set; }
    }
}
