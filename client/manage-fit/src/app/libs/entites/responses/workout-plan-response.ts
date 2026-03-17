import { GetClientResponse } from "./client-response"

export interface GetWorkoutPlanByTrainerIdResponse {
    name: string,
    id: string,
    exercises: GetWorkoutPlanExerciseResponse[],
    clients: GetClientResponse[]
}

export interface GetWorkoutPlanExerciseResponse {
    name: string,
    description: string,
    videoUrl: string,
    set: 0,
    time: 0,
    repetition: 0,
    id: string
}

export interface AddWorkoutPlanResponse {
}

export interface DeleteWorkoutPlanResponse {
    naeme: string
}