import { Component, Input, OnInit } from '@angular/core';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { MatButtonModule } from '@angular/material/button';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatIconModule } from '@angular/material/icon';
import { MatInputModule } from '@angular/material/input';
import { ClientsService } from '../../services/clients/clients.service';
import { GetClientResponse } from '../../entites/responses/client-response';

@Component({
  selector: 'app-client',
  standalone: true,
  imports: [
    MatInputModule,
    MatFormFieldModule,
    FormsModule,
    ReactiveFormsModule,
    MatIconModule,
    MatButtonModule],
  templateUrl: './client.component.html',
  styleUrl: './client.component.scss'
})
export class ClientComponent implements OnInit {
  
  public client: GetClientResponse | undefined;

  @Input()
  set id(clientUid: string) {
    this.clientsService.getClient(clientUid)
    .subscribe(client =>{
      this.client = client;
    });
  }

  constructor(public clientsService: ClientsService){}

  ngOnInit(): void {}
} 
