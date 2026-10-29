export interface GymLocationResponse {
  id: string;
  address: string;
}

export interface GetGymResponse {
  id: string;
  name: string;
  locations: GymLocationResponse[];
}
