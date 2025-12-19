export interface AddExerciseRequest {
    name: string;
    description: string;
    videoUrl: string;
    trainerId: string;
}

export interface EditExerciseRequest {
    name: string;
    description: string;
    videoUrl: string;
}