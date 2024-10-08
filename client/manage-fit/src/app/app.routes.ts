import { Routes } from '@angular/router';
import { ClientsComponent } from './libs/clients/clients.component';
import { NutritionsComponent } from './libs/nutritions/nutritions.component';
import { WorkoutsComponent } from './libs/workouts/workouts.component';

export const routes: Routes = [
    {path: 'clients', component: ClientsComponent },
    {path: 'nutritions', component: NutritionsComponent },
    {path: 'workouts', component: WorkoutsComponent },
    { path: '**',   redirectTo: 'clients'}
];
