import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { AddClientResponse, DeleteClientResponse, EditClientResponse, GetClientResponse } from '../../entites/responses/client-response';
import { AddClientRequest, EditClientRequest } from '../../entites/requests/client-request';

@Injectable({
  providedIn: 'root'
})
export class ClientsService {

  constructor(public http: HttpClient) { }

  public getClients(trainerId: string): Observable<GetClientResponse[]>{
    return this.http.get<GetClientResponse[]>(`https://localhost:7003/api/clients/trainer/${trainerId}`);
  }

  public getClient(clientId: string): Observable<GetClientResponse>{
    return this.http.get<GetClientResponse>(`https://localhost:7003/api/clients/${clientId}`);
  }

  public deleteClient(id: string): Observable<DeleteClientResponse>{
    return this.http.delete<GetClientResponse>(`https://localhost:7003/api/clients/${id}`);
  }

  public addClient(clientRequest: AddClientRequest): Observable<AddClientResponse>{
    return this.http.post<AddClientResponse>(`https://localhost:7003/api/clients`, clientRequest);
  }

  public editClient(id: number, editClientRequest: EditClientRequest): Observable<EditClientResponse>{
    return this.http.put<GetClientResponse>(`https://localhost:7003/api/clients/${id}`, editClientRequest);
  }
}
