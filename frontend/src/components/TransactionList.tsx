import type { Transaction } from "../types/Transaction";
import { TransactionType } from "../types/Transaction";

interface TransactionListProps {
    transactions: Transaction[];
}

export function TransactionList({ transactions}: TransactionListProps){
    if(transactions.length === 0){
        return <p>Nenhuma transação encontrada</p>;
    }

    return (
        <div>
            <h2>Transações</h2>
            <table>
                <thead>
                    <tr>
                        <th>Descrição</th>
                        <th>Valor</th>
                        <th>Tipo</th>
                        <th>Pessoa</th>
                    </tr>
                </thead>
                <tbody>
                    {transactions.map((transaction) =>(
                        <tr key={transaction.id}>
                            <td>{transaction.description}</td>
                            <td>{transaction.amount.toFixed(2)}</td>
                            <td>{transaction.type === TransactionType.Income ? "Receita" : "Despesa"}</td>
                            <td>{transaction.personName}</td>
                        </tr>
                    ))}
                </tbody>
            </table>
        </div>
    )
}