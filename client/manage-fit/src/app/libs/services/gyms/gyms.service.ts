import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { GetGymResponse } from '../../entites/responses/gym-response';

@Injectable({
  providedIn: 'root'
})
export class GymsService {

  constructor(public http: HttpClient) { }

  public getGymForTrainer(): Observable<GetGymResponse> {
    return this.http.get<GetGymResponse>(`https://localhost:7003/api/gyms/trainer`);
  }
}
