import { useState } from "react";
import type { CreatePersonDto } from "../types/Person";


interface PersonFormProps {
    onPersonCreated: (dto: CreatePersonDto) => Promise<void>;
}

export function PersonForm({ onPersonCreated}: PersonFormProps) {
    const [name, setName] = useState("");
    const [age, setAge] = useState("");
    const [error, setError] = useState<string | null>(null);
    const [isLoading, setIsLoading] = useState(false);

    async function handleSubmit(event: React.FormEvent) {
        event.preventDefault();
        setError(null);
        setIsLoading(true);

        try {
            await onPersonCreated({ name, age: Number(age) });
            setName("");
            setAge("");
        } catch(e){
            setError(e instanceof Error ? e.message : "Erro creating person");
        } finally {
            setIsLoading(false);
        }
    }

    return (
        <form onSubmit={handleSubmit}>
            <h2>Cadastrar Pessoa</h2>
            <div>
                <label htmlFor="name">Nome</label>
                <input type="text" id="name" value={name} onChange={(e) => setName(e.target.value)} />
            </div>

            <div>
                <label htmlFor="age">Idade</label>
                <input type="number" id="age" value={age} onChange={(e) => setAge(e.target.value)} required min={0} max={100} />
            </div>

            {error && <p className="error">{error}</p>}

            <button type="submit" disabled={isLoading}>{isLoading ? "Cadastrando..." : "Cadastrar"}</button>
        </form>
    )
}