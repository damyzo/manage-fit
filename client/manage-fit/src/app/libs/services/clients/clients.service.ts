import { Injectable } from '@angular/core';
import { GetClientResult } from '../../entites/responses/client-response';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class ClientsService {

  constructor(public http: HttpClient) { }

  public getClients(): Observable<GetClientResult>{
    return this.http.get<GetClientResult>('https://localhost:7003/api/clients')
  }

  public deleteClient(uid: string): Observable<GetClientResult>{
    return this.http.delete<GetClientResult>(`https://localhost:7003/api/clients/${uid}`)
  }
}
