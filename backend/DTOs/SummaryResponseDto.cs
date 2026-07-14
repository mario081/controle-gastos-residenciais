namespace Expense.Api.Dtos
{
    public class SummaryResponseDto
    {
        public List<PersonSummaryDto> People { get; set; } = new();
        public decimal TotalIncome { get; set; }
        public decimal TotalExpense { get; set; }
        public decimal Balance { get; set; }
    }
}
