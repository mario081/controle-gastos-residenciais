using Expense.Api.Models;

namespace Expense.Api.Dtos
{
    public class TransactionResponseDto
    {
        public int Id { get; set; }
        public string Description { get; set; } = string.Empty;
        public decimal Amount { get; set; }
        public TransactionType Type { get; set; }
        public int PersonId { get; set; }
        public string PersonName { get; set; } = string.Empty;
    }
}
