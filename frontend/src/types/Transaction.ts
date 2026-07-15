// Enum para o tipo de transação
export enum TransactionType {
    Income = 0,
    Expense = 1,
}

// Interface para a transação
export interface Transaction {
    id: number;
    description: string;
    amount: number;
    type: TransactionType;
    personId: number;
    personName: string;
}

export interface CreateTransactionDto {
    description: string;
    amount: number;
    type: TransactionType;
    personId: number; 
}