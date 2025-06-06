import { Component, inject, OnInit } from '@angular/core';
import { AddClientDialogInterface } from '../../../entites/clients/add-client-dialog-interface';
import {FormBuilder, FormControl, FormGroup, FormsModule, ReactiveFormsModule, Validators} from '@angular/forms';
import {MatButtonModule} from '@angular/material/button';
import {
  MAT_DIALOG_DATA,
  MatDialogActions,
  MatDialogClose,
  MatDialogContent,
  MatDialogRef,
  MatDialogTitle,
} from '@angular/material/dialog';
import {MatFormFieldModule} from '@angular/material/form-field';
import {MatInputModule} from '@angular/material/input';
import { ClientsService } from '../../../services/clients/clients.service';
import { AddClientRequest } from '../../../entites/requests/client-request';
import { DialogStatics, ErrorMessages } from '../../../statics/statics';
import { NotificationService } from '../../../services/notification/notification.service';
@Component({
  selector: 'app-add-client-dialog',
  standalone: true,
  imports: [
    MatFormFieldModule,
    MatInputModule,
    FormsModule,
    ReactiveFormsModule,
    MatButtonModule,
    MatButtonModule,
    MatDialogActions,
    MatDialogContent,
    MatDialogTitle
],
  templateUrl: './add-client-dialog.component.html',
  styleUrl: './add-client-dialog.component.scss'
})
export class AddClientDialogComponent implements OnInit {
  
  readonly dialogRef = inject(MatDialogRef<AddClientDialogComponent>);
  readonly data = inject<AddClientDialogInterface>(MAT_DIALOG_DATA);

  public form: FormGroup = new FormGroup({
    name: new FormControl(''),
    email: new FormControl(''),
    weight: new FormControl(''),
    height: new FormControl('')
  });
  constructor(
    private fb: FormBuilder,
    public clientsService: ClientsService,
    private notificationService: NotificationService) {}
  
  save():void
  {
    let request: AddClientRequest = {
      name: this.form.get('name')?.value,
      email: this.form.get('email')?.value,
      weight: this.form.get('weight')?.value,
      height: this.form.get('height')?.value,
      trainerId: '0D913FF2-7D78-4B07-993B-12D21D570A54'
    };

    this.clientsService.addClient(request).subscribe({
        error: (err) => {
          this.notificationService.error(ErrorMessages.InsertError + err.status);
        },
        next: () => {
          this.notificationService.success(ErrorMessages.InsertSuccess);
        },
        complete: () => {
          this.dialogRef.close(DialogStatics.SUCCESS);
        }
    }); 
  }

  close(): void {
    this.dialogRef.close(DialogStatics.CLOSED);
  }

  ngOnInit(): void {
    this.form = this.fb.group({

      name: ['', Validators.required],

      email: ['', [Validators.required, Validators.email]],

      weight: [null, [Validators.required, Validators.min(0)]],

      height: [null, [Validators.required, Validators.min(0)]]

    });
  }
}
