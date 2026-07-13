namespace Expense.Api.Models
{
    public enum TransactionType
    {
        Income,
        Expense
    }

    // Gasto ou receita ligado a uma pessoa.
    public class Transaction
    {
        public int Id { get; set; }
        public string Description { get; set; } = string.Empty;
        public decimal Amount { get; set; }
        public TransactionType Type { get; set; }

        public int PersonId { get; set; }
        public Person? Person { get; set; }
    }
}
