namespace Expense.Api.Dtos
{
    public class PersonSummaryDto
    {
        public int PersonId { get; set; }
        public string PersonName { get; set; } = string.Empty;
        public decimal TotalIncome { get; set; }
        public decimal TotalExpense { get; set; }
        public decimal Balance { get; set; }
    }
}
