export interface AddWorkoutPlanRequest {
    name: string,
    trainerId: string,
    exerciseIds: AddWorkoutPlanExerciseRequest[],
    clientIds: string[]
}

export interface AddWorkoutPlanExerciseRequest {
    id: string | null,
    name: string,
    description: string,
    videoUrl: string
}