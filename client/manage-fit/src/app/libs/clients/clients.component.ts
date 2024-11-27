import { Component, OnInit } from '@angular/core';
import { GetClientResponse } from '../entites/responses/client-response';
import {MatCardModule} from '@angular/material/card';
import {MatInputModule} from '@angular/material/input';
import {MatFormFieldModule} from '@angular/material/form-field';

import {
  FormControl,
  FormsModule,
  ReactiveFormsModule,
} from '@angular/forms';
import { ClientsService } from '../services/clients/clients.service';
import { MatIconModule } from '@angular/material/icon';
import { MatButtonModule } from '@angular/material/button';
import { switchMap } from 'rxjs';
import { RouterLink } from '@angular/router';

@Component({
  selector: 'app-clients',
  standalone: true,
  imports: [
    RouterLink,
    MatCardModule,
    MatInputModule,
    MatFormFieldModule,
    FormsModule,
    ReactiveFormsModule,
    MatIconModule,
    MatButtonModule
  ],
  templateUrl: './clients.component.html',
  styleUrl: './clients.component.scss'
})
export class ClientsComponent implements OnInit {

  public clients: GetClientResponse[] = [];
  public filteredClients: GetClientResponse[] = [];

  searchFormControl = new FormControl('', []);

  constructor(public clientsService: ClientsService){}
  
  ngOnInit(): void {
    this.clientsService.getClients('3FA85F64-5717-4562-B3FC-2C963F66AFA6')
    .subscribe((data) => {
      this.clients = data;
      this.filteredClients = data;
    });

    this.searchFormControl.valueChanges.subscribe((value) => { 
      this.filteredClients = this.clients.filter(client => {
        return client.email.match(value??'') || client.name.match(value??''); 
      })
    })
  }

  public openAddClientDialog(){

  }

  public deleteClientDialog(id: string){
    this.clientsService
    .deleteClient(id)
      .pipe(switchMap(() => {
        return this.clientsService.getClients('3FA85F64-5717-4562-B3FC-2C963F66AFA6');
      })).subscribe((data) => {
        this.clients = data;
        this.filteredClients = data;
      });
  }
}
