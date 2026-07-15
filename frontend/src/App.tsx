import { useState, useEffect } from "react";
import type { Person, CreatePersonDto } from "./types/Person";
import type { Transaction, CreateTransactionDto } from "./types/Transaction";
import type { Summary } from "./types/Summary";
import { getPeople, createPerson, deletePerson } from "./services/personService";
import {
  getTransactions,
  createTransaction,
} from "./services/transactionService";
import { getSummary } from "./services/summaryService";
import { PersonForm } from "./components/PersonForm";
import { PersonList } from "./components/PersonList";
import { TransactionForm } from "./components/TransactionForm";
import { TransactionList } from "./components/TransactionList";
import { SummaryTable } from "./components/SummaryTable";

function App() {
  // Estado global da aplicação: guarda os dados vindos da API.
  // Cada "useState" cria uma variável reativa - quando ela muda,
  // o React re-renderiza automaticamente os componentes que a usam.
  const [people, setPeople] = useState<Person[]>([]);
  const [transactions, setTransactions] = useState<Transaction[]>([]);
  const [summary, setSummary] = useState<Summary | null>(null);
  const [loading, setLoading] = useState(true);

  // Função central que busca TODOS os dados da API de uma vez.
  // Reaproveitamos ela sempre que algo muda (criar pessoa, criar
  // transação, deletar pessoa) para manter a tela sempre atualizada.
  async function loadData() {
    try {
      // Promise.all dispara as 3 chamadas em PARALELO (não uma
      // esperando a outra terminar), o que é mais rápido do que
      // usar 3 "await" separados em sequência.
      const [peopleData, transactionsData, summaryData] = await Promise.all([
        getPeople(),
        getTransactions(),
        getSummary(),
      ]);

      setPeople(peopleData);
      setTransactions(transactionsData);
      setSummary(summaryData);
    } catch (err) {
      console.error("Erro ao carregar dados:", err);
    } finally {
      setLoading(false);
    }
  }

  // useEffect com array de dependências vazio ([]) roda a função
  // UMA VEZ, logo após o componente ser montado na tela pela primeira
  // vez - é o equivalente a um "ngOnInit" do Angular, ou ao construtor
  // de um Service que já carrega dados ao iniciar.
  useEffect(() => {
    loadData();
  }, []);

  // Handler chamado pelo PersonForm quando uma pessoa é criada.
  // Depois de criar, recarrega TODOS os dados (a pessoa nova precisa
  // aparecer na lista, e o summary também muda, já que tem uma pessoa
  // nova com saldo zerado).
  async function handlePersonCreated(data: CreatePersonDto) {
    await createPerson(data);
    await loadData();
  }

  // Handler chamado pelo PersonList quando o botão "Excluir" é clicado.
  async function handleDeletePerson(id: number) {
    await deletePerson(id);
    await loadData();
  }

  // Handler chamado pelo TransactionForm quando uma transação é criada.
  async function handleTransactionCreated(data: CreateTransactionDto) {
    await createTransaction(data);
    await loadData();
  }

  // Enquanto os dados ainda não chegaram da API, mostra um texto simples
  // em vez de telas vazias/quebradas.
  if (loading) {
    return <p>Carregando...</p>;
  }

  return (
    <div style={{ maxWidth: 900, margin: "0 auto", padding: "1rem" }}>
      <h1>Controle de Gastos Residenciais</h1>

      <section>
        <PersonForm onPersonCreated={handlePersonCreated} />
        <PersonList persons={people} onDeletePerson={handleDeletePerson} />
      </section>

      <hr />

      <section>
        <TransactionForm
          persons={people}
          onTransactionCreated={handleTransactionCreated}
        />
        <TransactionList transactions={transactions} />
      </section>

      <hr />

      <section>
        {/* summary pode ser null antes de carregar, então só
            renderizamos a tabela quando ele realmente existir. */}
        {summary && <SummaryTable summary={summary} />}
      </section>
    </div>
  );
}

export default App;