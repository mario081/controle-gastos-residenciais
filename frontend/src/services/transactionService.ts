import { URL_BASE_API } from "./api";
import type { Transaction, CreateTransactionDto } from "../types/Transaction";

export async function getTransactions(): Promise<Transaction[]> {
    const response = await fetch(`${URL_BASE_API}/Transactions`);
    if(!response.ok){
        throw new Error("Failed to fetch transactions");
    }
    return response.json();
}

export async function Transaction(dto: CreateTransactionDto): Promise<Transaction> {
    const response = await fetch(`${URL_BASE_API}/Transactions`, {
        method: "POST",
        headers: {
            "Content-Type": "application/json", 
    },
    body: JSON.stringify(dto),
    });

    if(!response.ok){
        const errorDto = await response.json().catch(() => null);
        throw new Error(errorDto?.message ?? "Failed to create transaction");
    }

    return response.json();
}