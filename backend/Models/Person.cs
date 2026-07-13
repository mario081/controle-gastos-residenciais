namespace Expense.Api.Models
{
    // Pessoa do controle de gastos.
    public class Person
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public int Age { get; set; }

        public List<Transaction> Transactions { get; set; } = new();
    }
}
