namespace Expense.Api.Dtos
{
    // Saída da API sem expor a navegação Transactions.
    public class PersonResponseDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public int Age { get; set; }
    }
}
