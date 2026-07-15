import type { Person } from "../types/Person";

interface PersonListProps {
    persons: Person[];
    onDeletePerson: (id: number) => Promise<void>;
}

export function PersonList({ persons, onDeletePerson}: PersonListProps){
    if(persons.length === 0){
        return <p>Nenhuma pessoa cadastrada</p>
    }
    return (
        <div>
            <h2>Pessoas Cadastradas</h2>
            <table>
                <thead>
                    <tr>
                        <th>Nome</th>
                        <th>Idade</th>
                        <th>Ações</th>
                    </tr>
                </thead>
                <tbody>
                    {persons.map((person) => (
                        <tr key={person.id}>
                            <td>{person.name}</td>
                            <td>{person.age}</td>
                            <td>
                                <button onClick={() => onDeletePerson(person.id)}>Deletar</button>
                            </td>
                        </tr>
                    ))}
                </tbody>
            </table>
        </div>
    )
}