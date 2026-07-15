// Interface para a pessoa
export interface Person {
    id: number;
    name: string;
    age: number;
}

export interface CreatePersonDto {
    name: string;
    age: number;
}