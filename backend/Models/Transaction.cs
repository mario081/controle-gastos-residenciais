namespace expense.api.Models {

    // Enum que vai ter os tipos de transação.
    public enum TransactionType{
        Income,
        Expense
    }

    // Classe que vai ter as informações da transação.
    public class Transaction {

        // Identificador único.
        public int Id { get; set;}

        // Descrição da transação.
        public string Description { get; set;} = string.Empty;

        // Valor da transação.
        public decimal Amount { get; set;}

        // Tipo da transação.
        public TransactionType Type { get; set;}

        // Identificador da pessoa.
        public int PersonId { get; set;}

        // Pessoa que realizou a transação.
        public Person? Person { get; set;}
    }
}