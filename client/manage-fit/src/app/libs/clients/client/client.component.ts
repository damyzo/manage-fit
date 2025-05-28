import { Component, Input, OnInit } from '@angular/core';
import { FormBuilder, FormControl, FormGroup, FormsModule, ReactiveFormsModule, Validators } from '@angular/forms';
import { MatButtonModule } from '@angular/material/button';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatIconModule } from '@angular/material/icon';
import { MatInputModule } from '@angular/material/input';
import { ClientsService } from '../../services/clients/clients.service';
import { GetClientResponse } from '../../entites/responses/client-response';
import { ActivatedRoute } from '@angular/router';
import { switchMap, tap } from 'rxjs';
import { NotificationService } from '../../services/notification/notification.service';
import { ErrorMessages } from '../../statics/statics';

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
  public form: FormGroup = new FormGroup({
    name: new FormControl(''),
    email: new FormControl(''),
    weight: new FormControl(''),
    height: new FormControl('')
  });

  constructor(
    private clientsService: ClientsService,
    private fb: FormBuilder,
    private route: ActivatedRoute,
    private notificationService: NotificationService
  ){}

  ngOnInit(): void {
    this.route.params.pipe(
      switchMap(params => {
      const clientId = params['id'];
      return this.clientsService.getClient(clientId)
    }),
    tap((data: GetClientResponse) => {
      this.client = data;

      this.form = this.fb.group({
          id: [this.client?.id],
    
          name: [this.client?.name, Validators.required],
    
          email: [this.client?.email, [Validators.required, Validators.email]],
    
          weight: [this.client?.weight, [Validators.required, Validators.min(0)]],
    
          height: [this.client?.height, [Validators.required, Validators.min(0)]]
      });
    })).subscribe();
  }
  

  save(): void {
    const clinetId = this.form.get('id')?.value;

    if(this.form.valid && clinetId){
      let request = {
        name: this.form.get('name')?.value,
        email: this.form.get('email')?.value,
        weight: this.form.get('weight')?.value,
        height: this.form.get('height')?.value
      };

      this.clientsService.editClient(clinetId, request)
        .subscribe({
          error: (err) => {
            this.notificationService.error(ErrorMessages.UpdateError + err.message);
          },
          complete: () => {
            this.notificationService.success(ErrorMessages.UpdateSuccess);
          }
        });
    }
  }

  back(): void {
    window.history.back();
  }
} 
