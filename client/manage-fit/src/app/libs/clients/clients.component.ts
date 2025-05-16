import { Component, inject, OnInit } from '@angular/core';
import { AddClientResponse, GetClientResponse } from '../entites/responses/client-response';
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
import { BehaviorSubject, Observable, switchMap, tap } from 'rxjs';
import { RouterLink } from '@angular/router';
import { MatDialog } from '@angular/material/dialog';
import { AddClientDialogComponent } from './dialogs/add-client-dialog/add-client-dialog.component';
import { AddClientRequest } from '../entites/requests/client-request';

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

  readonly dialog = inject(MatDialog);
  private dialogRef: any;
  public clients: GetClientResponse[] = [];
  public filteredClients: GetClientResponse[] = [];
  private subject = new BehaviorSubject(null);

  searchFormControl = new FormControl('', []);

  constructor(public clientsService: ClientsService){}
  
  ngOnInit(): void {
    this.subject
      .pipe(switchMap(() => {
        return this.clientsService.getClients('0D913FF2-7D78-4B07-993B-12D21D570A54');
      }),
      tap((data: GetClientResponse[]) => {
        this.clients = data;
        this.filteredClients = data;
      })
    ).subscribe();

    this.searchFormControl.valueChanges.subscribe((value) => { 
      this.filteredClients = this.clients.filter(client => {
        return client.email.match(value??'') || client.name.match(value??''); 
      })
    })
  }

  public openAddClientDialog(){
    this.dialogRef = this.dialog.open(AddClientDialogComponent, {});
    this.dialogRef.afterClosed()
    .pipe(
      switchMap((request: AddClientRequest) => {
        return this.clientsService.addClient(request); 
      }),
      tap(() => {
        this.subject.next(null);
      })).subscribe();
  }

  public deleteClientDialog(id: string){
    this.clientsService
    .deleteClient(id)
      .pipe(tap(() => {
        this.subject.next(null);
      })).subscribe();
  }
}
