export interface AddClientRequest
{
    name: string;
    weight: number,
    height: number,
    email: string,
    dateOfBirth?: string | null,
    gymLocationIds?: string[]
}

export interface EditClientRequest{
    name: string;
    weight: number,
    height: number,
    email: string,
    dateOfBirth?: string | null,
    gymLocationIds?: string[]
}
