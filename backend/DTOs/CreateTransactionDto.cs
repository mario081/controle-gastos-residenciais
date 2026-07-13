using System.ComponentModel.DataAnnotations;

namespace ExpenseTracker.DTOs;

// DTO para criar uma transação

public class CreateTransactionDto
{
    [Required(ErrorMenssage = " The description is required")]
    public string Description { get; set; } = string.Empty

    [Required(ErrorMessage = "The amount is required")]
    public decimal Amount { get; set; }

    [Required(ErrorMessage = "The person ID is required")]
    public int PersonId { get; set; }
}