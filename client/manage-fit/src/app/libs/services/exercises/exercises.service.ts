import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { AddExerciseResponse, DeleteExerciseResponse, EditExerciseResponse, GetExerciseResponse } from '../../entites/responses/exercise-response';
import { AddExerciseRequest, EditExerciseRequest } from '../../entites/requests/exercise-request';

@Injectable({
  providedIn: 'root'
})
export class ExercisesService {

  constructor(public http: HttpClient) { }

  public getExercises(trainerId: string): Observable<GetExerciseResponse[]>{
    return this.http.get<GetExerciseResponse[]>(`https://localhost:7003/api/exercises/trainer/${trainerId}`);
  }

  public getExercise(exerciseId: string): Observable<GetExerciseResponse>{
    return this.http.get<GetExerciseResponse>(`https://localhost:7003/api/exercises/${exerciseId}`);
  }

  public deleteExercise(id: string): Observable<DeleteExerciseResponse>{
    return this.http.delete<DeleteExerciseResponse>(`https://localhost:7003/api/exercises/${id}`);
  }

  public addExercise(exerciseRequest: AddExerciseRequest): Observable<AddExerciseResponse>{
    return this.http.post<AddExerciseResponse>(`https://localhost:7003/api/exercises`, exerciseRequest);
  }

  public editExercise(id: number, editExerciseRequest: EditExerciseRequest): Observable<EditExerciseResponse>{
    return this.http.put<GetExerciseResponse>(`https://localhost:7003/api/exercises/${id}`, editExerciseRequest);
  }
}

