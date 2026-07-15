import { URL_BASE_API} from "./api";
import type { Person, CreatePersonDto } from "../types/Person";
import { CreateTransactionDto } from "../types/Transaction";
import { PersonSummary } from "../types/Summary";

export async function getPeople(): Promise<Person[]> {

    const response = await fetch(`${URL_BASE_API}/person`);

    if(!response.ok){
        throw new Error("Failed to fetch people");
    }

    return response.json();
}

export async function createPerson(dto: CreateTransactionDto): Promise<Person> {
    const response = await fetch(`${URL_BASE_API}`, {
        method: "Post",
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

export async function getPersonSummary(id: number): Promise<void> {
    const response = await fetch(`${URL_BASE_API}/Paople/${id}`, {
        method: "DELETE",
    });

    if(!response.ok){
        throw new Error("Failed to delete person");
    }
}