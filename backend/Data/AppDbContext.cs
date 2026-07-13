using Microsoft.EntityFrameworkCore;
using expense.api.Models;

namespace expense.api.data {

    // Classe que vai fazer a ponte entre o C# e o banco de dados.
    public class AppDbContext : DbContext {

        // Construtor da classe para injeção de dependência.
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) {}

        // Pegando os models de Person e transformando em tabelas no banco de dados.
        public DbSet<Person> People { get; set;}

        // Pegando os models de Transaction e transformando em tabelas no banco de dados.
        public DbSet<Transaction> Transactions { get; set;}

        // Pegando os models de Person e Transaction e transformando em tabelas no banco de dados.
        protected override void OnModelCreating(ModelBuilder modelBuilder) {
            modelBuilder.Entity<Person>()
                .HasMany(p => p.Transactions)
                .WithOne(t => t.Person)
                .HasForeignKey(t => t.PersonId)
                // Deletando a pessoa e todas as transações relacionadas a ela.
                .OnDelete(DeleteBehavior.Cascade);

                base.OnModelCreating(modelBuilder);
        }
    }
}