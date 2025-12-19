import { Component, inject, OnInit } from '@angular/core';
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
import { DialogStatics, ErrorMessages } from '../../../statics/statics';
import { NotificationService } from '../../../services/notification/notification.service';
import { AddExerciseDialogInterface } from '../../../entites/exercises/add-exercise-dialog-interface';
import { AddExerciseRequest } from '../../../entites/requests/exercise-request';
import { ExercisesService } from '../../../services/exercises/exercises.service';
@Component({
  selector: 'app-add-exercise-dialog',
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
  templateUrl: './add-exercise-dialog.component.html',
  styleUrl: './add-exercise-dialog.component.scss'
})
export class AddExerciseDialogComponent  implements OnInit {
  
  readonly dialogRef = inject(MatDialogRef<AddExerciseDialogComponent>);
  readonly data = inject<AddExerciseDialogInterface>(MAT_DIALOG_DATA);

  public form: FormGroup = new FormGroup({
    name: new FormControl(''),
    videoUrl: new FormControl(''),
    descritpion: new FormControl(''),
  });
  constructor(
    private fb: FormBuilder,
    public exercisesService: ExercisesService,
    private notificationService: NotificationService) {}
  
  save():void
  {
    let request: AddExerciseRequest = {
      name: this.form.get('name')?.value,
      videoUrl: this.form.get('videoUrl')?.value,
      description: this.form.get('description')?.value,
      trainerId: '3B161710-B6AD-4F9D-ADF4-6DA489156044'
    };

    this.exercisesService.addExercise(request).subscribe({
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

      videoUrl: ['', [Validators.required]],

      description: [null, [Validators.required]],

    });
  }
}
