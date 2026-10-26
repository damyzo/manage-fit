import { Component, DestroyRef, inject } from '@angular/core';
import { FormControl, FormGroup, FormsModule, ReactiveFormsModule, Validators } from '@angular/forms';
import { MatButtonModule } from '@angular/material/button';
import {
  MatDialogActions,
  MatDialogContent,
  MatDialogRef,
  MatDialogTitle,
} from '@angular/material/dialog';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';
import { takeUntilDestroyed } from '@angular/core/rxjs-interop';
import { WorkoutPlansService } from '../../../services/workout-plans/workout-plans.service';
import { NotificationService } from '../../../services/notification/notification.service';
import { AddWorkoutPlanRequest } from '../../../entites/requests/workout-plan-request';
import { DialogStatics, ErrorMessages } from '../../../statics/statics';

@Component({
  selector: 'app-add-workout-plan-dialog',
  standalone: true,
  imports: [
    MatFormFieldModule,
    MatInputModule,
    FormsModule,
    ReactiveFormsModule,
    MatButtonModule,
    MatDialogActions,
    MatDialogContent,
    MatDialogTitle
  ],
  templateUrl: './add-workout-plan-dialog.component.html',
  styleUrl: './add-workout-plan-dialog.component.scss'
})
export class AddWorkoutPlanDialogComponent {
  private readonly destroyRef = inject(DestroyRef);

  readonly dialogRef = inject(MatDialogRef<AddWorkoutPlanDialogComponent>);

  public form: FormGroup = new FormGroup({
    name: new FormControl('', Validators.required)
  });

  constructor(
    private workoutPlansService: WorkoutPlansService,
    private notificationService: NotificationService) {}

  save(): void {
    if (this.form.invalid) {
      return;
    }

    const request: AddWorkoutPlanRequest = {
      name: this.form.get('name')?.value,
      trainerId: '3B161710-B6AD-4F9D-ADF4-6DA489156044',
      exerciseIds: [],
      clientIds: []
    };

    this.workoutPlansService.addWorkoutPlan(request)
      .pipe(takeUntilDestroyed(this.destroyRef))
      .subscribe({
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
}
