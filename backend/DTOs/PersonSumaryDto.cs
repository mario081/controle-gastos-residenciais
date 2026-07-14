namespace Expense.Api.Dtos{
    // Dto de Sumario de Pessoa
    public class PersonSumaryDto{
        public int PersonId { get; set;}
        public string PersonName { get; set;} = string.Empty;
        public int TotalIncome { get; set;}
        public int TotalExpense { get; set;}
        public int Balance { get; set;}

    }
}