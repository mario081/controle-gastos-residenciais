using System.ComponentModel.DataAnnotations;

namespace ExpenseTracker.DTOs;

// DTO para resposta de uma transação
public class TransactionResponseDto{

    public int Id { get; set;}

    public string Description { get; set; } = string.Empty;

    public decimal Amount { get; set; }

    public TransactionType Type { get; set;}

    public int PersonId { get; set; }

    public strint PersonName { get; set.} = string.Empty;
}