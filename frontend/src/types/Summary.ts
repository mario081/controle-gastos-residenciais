// Interface para o resumo da pessoa
export interface PersonSummary {
    personId: number;
    personName: string;
    totalIncome: number;
    totalExpense: number;
    balance: number;
}

export interface Summary{
    people: PersonSummary[];
    totalIncome: number;
    totalExpense: number;
    balance: number;
}