namespace Expense.Api.Dtos{
    // Dto de Resposta de Sumario
    public class SumaryResponseDto{
        public List<PersonSumaryDto> People { get; set;} = new List<PersonSumaryDto>();

        public int TotalIncome { get; set;}
        public int TotalExpense { get; set;}
        public int Balance { get; set;}
    }
}