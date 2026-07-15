import { URL_BASE_API} from "./api";
import type { Person, CreatePersonDto } from "../types/Person";

export async function getPeople(): Promise<Person[]> {

    const response = await fetch(`${URL_BASE_API}/People`);

    if(!response.ok){
        throw new Error("Failed to fetch people");
    }

    return response.json();
}

export async function createPerson(dto: CreatePersonDto): Promise<Person> {
    const response = await fetch(`${URL_BASE_API}/People`, {
        method: "POST",
        headers: {
            "Content-Type": "application/json",
        },
        body: JSON.stringify(dto),
    });

    if(!response.ok){
        const errorDto = await response.json().catch(() => null);
        throw new Error(errorDto?.message || "Failed to create person");
    }

    return response.json();
};

export async function deletePerson(id: number): Promise<void> {
    const response = await fetch(`${URL_BASE_API}/People/${id}`, {
        method: "DELETE",
    });

    if(!response.ok){
        throw new Error("Failed to delete person");
    }
}