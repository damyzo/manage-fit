import { HttpClient } from '@angular/common/http';
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

@Component({
  selector: 'app-clients',
  standalone: true,
  imports: [
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

  constructor(public http: HttpClient,
    public clientsService: ClientsService
  ){}
  
  ngOnInit(): void {
    this.clientsService.getClients().subscribe((data) => {
      this.clients = data.value;
      this.filteredClients = data.value;
    });

    this.searchFormControl.valueChanges.subscribe((value) => { 
      this.filteredClients = this.clients.filter(client => {
        return client.email.match(value??'') || client.name.match(value??''); 
      })
    })
  }

  public openAddClientDialog(){

  }

  public deleteClientDialog(uid: string){
    console.log(uid);
    this.clientsService.deleteClient(uid)
      .pipe(switchMap(() => {
        return this.clientsService.getClients();
      })).subscribe((data) => {
        this.clients = data.value;
        this.filteredClients = data.value;
      });
  }
}