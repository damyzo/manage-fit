import { Routes } from '@angular/router';
import { ClientsComponent } from './libs/clients/clients.component';
import { NutritionsComponent } from './libs/nutritions/nutritions.component';
import { WorkoutsComponent } from './libs/workouts/workouts.component';
import { ClientComponent } from './libs/clients/client/client.component';

export const routes: Routes = [
    { path: 'clients', component: ClientsComponent },
    { path: 'clients/:id', component: ClientComponent },
    { path: 'nutritions', component: NutritionsComponent },
    { path: 'workouts', component: WorkoutsComponent },
    { path: '**',   redirectTo: 'clients'}
];
