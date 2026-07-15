# Controle de Gastos Residenciais

Sistema de controle de gastos residenciais desenvolvido como desafio técnico. Permite cadastrar pessoas, registrar suas transações (receitas e despesas) e consultar totais individuais e gerais.

## Tecnologias utilizadas

**Back-end**
- .NET 10 / C#
- ASP.NET Core Web API
- Entity Framework Core
- SQLite (banco de dados relacional em arquivo, com persistência local)
- Swagger / OpenAPI (documentação e testes manuais dos endpoints)

**Front-end**
- React
- TypeScript
- Vite

## Funcionalidades

### Cadastro de pessoas
- Criação, listagem e exclusão de pessoas
- Ao excluir uma pessoa, todas as suas transações são excluídas automaticamente (cascade delete)

### Cadastro de transações
- Criação e listagem de transações (descrição, valor, tipo e pessoa vinculada)
- Validação: o `PersonId` informado precisa existir no cadastro de pessoas
- Regra de negócio: pessoas menores de 18 anos só podem cadastrar transações do tipo despesa

### Consulta de totais
- Lista todas as pessoas cadastradas com o total de receitas, despesas e saldo de cada uma
- Exibe o total geral (receitas, despesas e saldo líquido) somando todas as pessoas

## Como rodar o projeto

### Pré-requisitos
- [.NET SDK 10](https://dotnet.microsoft.com/download)
- [Node.js 18+](https://nodejs.org/)

### Back-end

```bash
cd backend
dotnet restore
dotnet ef database update   # cria o banco SQLite e aplica as migrations
dotnet run
```

A API estará disponível em `http://localhost:5187` (a porta exata é exibida no terminal ao iniciar).
A documentação interativa (Swagger) fica em `http://localhost:5187/swagger`.

### Front-end

Em outro terminal:

```bash
cd frontend
npm install
npm run dev
```

A aplicação estará disponível em `http://localhost:5173`.

> **Importante:** o back-end precisa estar rodando para que o front-end consiga carregar e enviar dados.

## Persistência de dados

Os dados são armazenados em um banco SQLite (`backend/controlegastos.db`), gerado automaticamente ao rodar as migrations. Os dados persistem mesmo após fechar a aplicação, já que não há uso de armazenamento em memória.

## Estrutura do projeto

```
controle-de-gasto/
├── backend/
│   ├── Controllers/     # Endpoints da API (Pessoas, Transações, Totais)
│   ├── Models/           # Entidades do banco de dados (Person, Transaction)
│   ├── Dtos/              # Objetos de entrada/saída da API
│   ├── Data/             # Configuração do Entity Framework (DbContext)
│   └── Program.cs       # Configuração da aplicação (rotas, CORS, banco)
├── frontend/
│   └── src/
│       ├── components/  # Componentes visuais (formulários, listas, tabela de totais)
│       ├── services/     # Funções de comunicação com a API
│       └── types/        # Interfaces TypeScript que espelham os DTOs do back-end
└── README.md
```

## Endpoints da API

| Método | Rota                  | Descrição                                  |
|--------|------------------------|---------------------------------------------|
| GET    | `/api/People`          | Lista todas as pessoas                       |
| POST   | `/api/People`          | Cadastra uma nova pessoa                     |
| DELETE | `/api/People/{id}`     | Remove uma pessoa e suas transações          |
| GET    | `/api/Transactions`    | Lista todas as transações                    |
| POST   | `/api/Transactions`    | Cadastra uma nova transação                  |
| GET    | `/api/Summary`         | Retorna o resumo de totais por pessoa e geral |

## Decisões técnicas

- **SQLite** foi escolhido por não exigir instalação de servidor de banco, mantendo o projeto simples de rodar em qualquer máquina, e por atender ao requisito de persistência em arquivo.
- **DTOs separados dos Models** para não expor a estrutura interna do banco diretamente na API e manter contratos de entrada/saída explícitos.
- **Cascade delete** configurado diretamente no banco de dados (via Entity Framework), garantindo integridade mesmo em acessos fora da API.
- Os comentários no código, em português, documentam a lógica de cada trecho.
