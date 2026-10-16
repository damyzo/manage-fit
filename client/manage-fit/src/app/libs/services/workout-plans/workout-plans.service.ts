import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { AddWorkoutPlanRequest } from '../../entites/requests/workout-plan-request';
import { AddWorkoutPlanResponse, DeleteWorkoutPlanResponse, GetWorkoutPlanByTrainerIdResponse } from '../../entites/responses/workout-plan-response';

@Injectable({
  providedIn: 'root'
})
export class WorkoutPlansService {

  constructor(public http: HttpClient) { }

  public getWorkoutPlansByTrainerId(trainerId: string): Observable<GetWorkoutPlanByTrainerIdResponse[]>{
    return this.http.get<GetWorkoutPlanByTrainerIdResponse[]>(`https://localhost:7003/api/workout-plans/trainer/${trainerId}`);
  }

  public getWorkoutPlan(id: string): Observable<GetWorkoutPlanByTrainerIdResponse>{
    return this.http.get<GetWorkoutPlanByTrainerIdResponse>(`https://localhost:7003/api/workout-plans/${id}`);
  }

  // public getClient(clientId: string): Observable<GetClientResponse>{
  //   return this.http.get<GetClientResponse>(`https://localhost:7003/api/clients/${clientId}`);
  // }

  public deleteWorkoutPlan(id: string): Observable<DeleteWorkoutPlanResponse>{
    return this.http.delete<DeleteWorkoutPlanResponse>(`https://localhost:7003/api/workout-plans/${id}`);
  }

  public addWorkoutPlan(clientRequest: AddWorkoutPlanRequest): Observable<AddWorkoutPlanResponse>{
    return this.http.post<AddWorkoutPlanResponse>(`https://localhost:7003/api/workout-plans`, clientRequest);
  }

  // public editClient(id: number, editClientRequest: EditClientRequest): Observable<EditClientResponse>{
  //   return this.http.put<GetClientResponse>(`https://localhost:7003/api/clients/${id}`, editClientRequest);
  // }
}
