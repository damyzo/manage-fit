import { Component, Inject, inject } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { MatButtonModule } from '@angular/material/button';
import { MAT_DIALOG_DATA, MatDialogActions, MatDialogContent, MatDialogRef, MatDialogTitle } from '@angular/material/dialog';
import { DialogStatics } from '../../../statics/statics';
import { WarningDialogData } from './interface/warning-dialog-models';

@Component({
  selector: 'app-warning-dialog-component',
  standalone: true,
  imports: [
    FormsModule,
    MatButtonModule,
    MatDialogActions,
    MatDialogContent,
    MatDialogTitle
  ],
  templateUrl: './warning-dialog-component.component.html',
  styleUrl: './warning-dialog-component.component.scss'
})
export class WarningDialogComponentComponent {

  readonly dialogRef = inject(MatDialogRef<WarningDialogComponentComponent>);
  
  constructor( @Inject(MAT_DIALOG_DATA) public data: WarningDialogData) {}

  no(): void {
    this.dialogRef.close(DialogStatics.CLOSED);
  }

  yes(): void {
    this.dialogRef.close(DialogStatics.SUCCESS);
  }

}
