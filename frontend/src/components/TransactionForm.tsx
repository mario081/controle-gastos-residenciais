import { useState } from "react";
import type { CreateTransactionDto } from "../types/Transaction";
import { TransactionType } from "../types/Transaction";
import { Person } from "../types/Person";

interface TransactionFormProps {
    persons: Person[];
    onTransactionCreated: (dto: CreateTransactionDto) => Promise<void>;
}

export function TransactionForm({ persons, onTransactionCreated}: TransactionFormProps) {
    const [ description, setDescription ] = useState("");
    const [ amount, setAmount ] = useState("");
    const [ type, setType] = useState<TransactionType>(TransactionType.Expense);
    const [ personId, setPersonId] = useState("");
    const [ error, setError] = useState<string | null>(null);
    const [ Loading, setLoading] = useState(false);

    async function handleSubmit(event: React.FormEvent) {
        event.preventDefault();
        setError(null);

        if (!personId){
            setError("People is required");
            return;
        }

        setLoading(true);

        try {
            await onTransactionCreated({
                description,
                amount: Number(amount),
                type,
                personId: Number(personId),
            });

            setDescription("");
            setAmount("");
            setType(TransactionType.Expense);
            setPersonId("");
        } catch(e){
            setError(e instanceof Error ? e.message : "Erro creating transaction");
        } finally {
            setLoading(false);
        }
    }

    return (
        <form onSubmit={handleSubmit}>
            <h2>Cadastrar Transação</h2>

            <div>
                <label htmlFor="description">Descrição</label>
                <input type="text" id="description" value={description} onChange={(e) => setDescription(e.target.value)} required />
            </div>

            <div>
                <label htmlFor="amount">Valor</label>
                <input type="number" id="amount" value={amount} onChange={(e) => setAmount(e.target.value)} required />
            </div>

            <div>
                <label htmlFor="type">Tipo</label>
                <select id="type" value={type} onChange={(e) => setType(Number(e.target.value) as TransactionType)}>
                    <option value={TransactionType.Expense}>Despesa</option>
                    <option value={TransactionType.Income}>Receita</option>
                </select>
            </div>

            <div>
                <label htmlFor="personId">Pessoa</label>
                <select id="personId" value={personId} onChange={(e) => setPersonId(e.target.value)}>
                    {persons.map((person) => (
                        <option key={person.id} value={person.id}>{person.name}</option>
                    ))}
                </select>
            </div>

            {error && <p style={{ color: "red" }}>{error}</p>}

            <button type="submit" disabled={Loading}>{Loading ? "Cadastrando..." : "Cadastrar"}</button>
        </form>
    )
}