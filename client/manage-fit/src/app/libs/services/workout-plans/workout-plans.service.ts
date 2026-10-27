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

  public getWorkoutPlansByTrainerId(): Observable<GetWorkoutPlanByTrainerIdResponse[]>{
    return this.http.get<GetWorkoutPlanByTrainerIdResponse[]>(`https://localhost:7003/api/workout-plans/trainer`);
  }

  public getWorkoutPlan(id: string): Observable<GetWorkoutPlanByTrainerIdResponse>{
    return this.http.get<GetWorkoutPlanByTrainerIdResponse>(`https://localhost:7003/api/workout-plans/${id}`);
  }

  public deleteWorkoutPlan(id: string): Observable<DeleteWorkoutPlanResponse>{
    return this.http.delete<DeleteWorkoutPlanResponse>(`https://localhost:7003/api/workout-plans/${id}`);
  }

  public addWorkoutPlan(clientRequest: AddWorkoutPlanRequest): Observable<AddWorkoutPlanResponse>{
    return this.http.post<AddWorkoutPlanResponse>(`https://localhost:7003/api/workout-plans`, clientRequest);
  }
}
