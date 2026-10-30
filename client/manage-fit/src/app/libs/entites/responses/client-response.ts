import { GymLocationResponse } from './gym-response';

export interface GetClientResponse{
    name: string;
    weight: number,
    height: number,
    email: string,
    dateOfBirth?: string | null,
    id: string,
    gymId?: string | null,
    gymLocations: GymLocationResponse[]
}

export interface GetClientsResponse{
    name: string;
    weight: number,
    height: number,
    email: string,
    dateOfBirth?: string | null,
    id: string,
    gymId?: string | null,
    gymLocations: GymLocationResponse[]
}

export interface AddClientResponse{
    name: string;
    weight: number,
    height: number,
    email: string
}

export interface EditClientResponse{
    name: string;
    weight: number,
    height: number,
    email: string
}

export interface DeleteClientResponse{
    name: string;
    weight: number,
    height: number,
    email: string
}
