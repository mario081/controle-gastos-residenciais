import type { Summary } from "../types/Summary";

interface SummaryTableProps {
    summary: Summary;
}

export function SummaryTable({ summary}: SummaryTableProps){
    return (
        <div>
            <h2>Totais</h2>
            <table>
                <thead>
                    <tr>
                        <th>Pessoa</th>
                        <th>Receita</th>
                        <th>Despesa</th>
                        <th>Saldo</th>
                    </tr>
                </thead>
                <tbody>
                    {summary.people.map((person) => (
                        <tr key={person.personId}>
                            <td>{person.personName}</td>
                            <td>{person.totalIncome.toFixed(2)}</td>
                            <td>{person.totalExpense.toFixed(2)}</td>
                            <td style={{ color: person.balance > 0 ? "green" : "red" }}>{person.balance.toFixed(2)}</td>
                        </tr>
                    ))}
                </tbody>
                <tfoot>
                    <tr>
                        <td>
                            <strong>Total</strong>
                        </td>
                        <td>
                            <strong>{summary.totalIncome.toFixed(2)}</strong>
                        </td>
                        <td>
                            <strong>{summary.totalExpense.toFixed(2)}</strong>
                        </td>
                        <td>
                            <strong style={{ color: summary.balance > 0 ? "green" : "red" }}>{summary.balance.toFixed(2)}</strong>
                        </td>
                    </tr>
                </tfoot>
            </table>
        </div>
    )
}