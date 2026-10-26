import { Routes } from '@angular/router';
import { ClientsComponent } from './libs/clients/clients.component';
import { NutritionsComponent } from './libs/nutritions/nutritions.component';
import { ClientComponent } from './libs/clients/client/client.component';
import { ExercisesComponent } from './libs/exercises/exercises/exercises.component';
import { ExerciseComponent } from './libs/exercises/exercise/exercise.component';
import { WorkoutPlansComponent } from './libs/workout-plans/workout-plans.component';
import { WorkoutPlanComponent } from './libs/workout-plans/workout-plan/workout-plan.component';
import { AddWorkoutPlanComponent } from './libs/workout-plans/add-workout-plan/add-workout-plan.component';
import { SinginPageComponent } from './libs/singin-page/singin-page.component';
import { loggedOutGuard } from './libs/guards/logged-out.guard';

export const routes: Routes = [
    { path: 'clients', component: ClientsComponent },
    { path: 'clients/:id', component: ClientComponent },
    { path: 'nutritions', component: NutritionsComponent },
    { path: 'workouts', component: WorkoutPlansComponent },
    { path: 'workouts/add', component: AddWorkoutPlanComponent },
    { path: 'workouts/:id', component: WorkoutPlanComponent },
    { path: 'exercises', component: ExercisesComponent },
    { path: 'exercises/:id', component: ExerciseComponent },
    { path: 'login', component:     SinginPageComponent, canActivate: [loggedOutGuard] },
    { path: '**',   redirectTo: 'login'}
];
