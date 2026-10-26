export interface AddWorkoutPlanRequest {
    name: string,
    trainerUid: string,
    exercises: AddWorkoutPlanExerciseRequest[],
    clientUids: string[]
}

export interface AddWorkoutPlanExerciseRequest {
    id: string | null,
    name: string,
    description: string,
    videoUrl: string,
    set: number,
    repetitions: number,
    time: number,
    order: number
}