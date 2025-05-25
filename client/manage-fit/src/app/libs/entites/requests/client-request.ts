export interface AddClientRequest
{
    name: string;
    weight: number,
    height: number,
    email: string,
    trainerId: string
}

export interface EditClientRequest{
    name: string;
    weight: number,
    height: number,
    email: string
}