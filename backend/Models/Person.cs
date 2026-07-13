namespace Expense.Api.Models {

    // Classe que vai ter as informações da pessoa.
    public class Person {

        // Identificador único, gerado automaticamente pelo banco de dados.
        public int Id { get; set;}
        // Nome da pessoa.
        public string Name { get; set;} = string.Empty;
        // Idade da pessoa.
        public int Age { get; set;}
        // Lista de transações da pessoa.
        public List<Transaction> Transactions { get; set;} = new List<Transaction>();
    }
}