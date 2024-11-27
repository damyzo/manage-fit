import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { DeleteClientResponse, GetClientResponse } from '../../entites/responses/client-response';

@Injectable({
  providedIn: 'root'
})
export class ClientsService {

  constructor(public http: HttpClient) { }

  public getClients(trainerId: string): Observable<GetClientResponse[]>{
    return this.http.get<GetClientResponse[]>(`https://localhost:7003/api/clients/trainer/${trainerId}`)
  }

  public getClient(clientId: string): Observable<GetClientResponse>{
    return this.http.get<GetClientResponse>(`https://localhost:7003/api/clients/${clientId}`)
  }

  public deleteClient(id: string): Observable<DeleteClientResponse>{
    return this.http.delete<GetClientResponse>(`https://localhost:7003/api/clients/${id}`)
  }
}
