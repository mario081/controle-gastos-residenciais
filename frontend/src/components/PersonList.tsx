import type { Person } from "../types/Person";

interface PersonListProps {
    people: Person[];
    onDeletePerson: (id: number) => Promise<void>;
}

export function PersonList({ people, onDeletePerson}: PersonListProps){
    if(people.length === 0){
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
                    {people.map((people) => (
                        <tr key={people.id}>
                            <td>{people.name}</td>
                            <td>{people.age}</td>
                            <td>
                                <button onClick={() => onDeletePerson(people.id)}>Deletar</button>
                            </td>
                        </tr>
                    ))}
                </tbody>
            </table>
        </div>
    )
}