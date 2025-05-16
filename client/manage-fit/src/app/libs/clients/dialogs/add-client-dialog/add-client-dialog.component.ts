import { Component, inject, OnInit } from '@angular/core';
import { AddClientDialogInterface } from '../../../entites/clients/add-client-dialog-interface';
import {FormBuilder, FormGroup, FormsModule, ReactiveFormsModule, Validators} from '@angular/forms';
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
    MatDialogClose,
    MatDialogContent,
    MatDialogTitle
  ],
  templateUrl: './add-client-dialog.component.html',
  styleUrl: './add-client-dialog.component.scss'
})
export class AddClientDialogComponent implements OnInit {
  
  readonly dialogRef = inject(MatDialogRef<AddClientDialogComponent>);
  readonly data = inject<AddClientDialogInterface>(MAT_DIALOG_DATA);

  public form: FormGroup = new FormGroup({});

  constructor(
    private fb: FormBuilder,
    private clientService: ClientsService) {}
  
  save():void
  {
    let request: AddClientRequest = {
      name: this.form.get('name')?.value,
      email: this.form.get('email')?.value,
      weight: this.form.get('weight')?.value,
      height: this.form.get('height')?.value,
      trainerId: '0D913FF2-7D78-4B07-993B-12D21D570A54'
    };

    this.dialogRef.close(request);
  }

  close(): void {
    this.dialogRef.close();
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
