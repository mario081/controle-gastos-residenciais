import { URL_BASE_API } from "./api";
import type { Summary } from "../types/Summary";

export async function getSummary(): Promise<Summary>{
    const response = await fetch(`${URL_BASE_API}/Summary`);

    if(!response.ok){
        throw new Error("Failed to fetch summary");
    }

    return response.json();

}