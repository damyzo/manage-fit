import { Component, inject, OnInit } from '@angular/core';
import {MatCardModule} from '@angular/material/card';
import {MatInputModule} from '@angular/material/input';
import {MatFormFieldModule} from '@angular/material/form-field';

import {
  FormControl,
  FormsModule,
  ReactiveFormsModule,
} from '@angular/forms';
import { MatIconModule } from '@angular/material/icon';
import { MatButtonModule } from '@angular/material/button';
import { BehaviorSubject, switchMap, tap } from 'rxjs';
import { RouterLink } from '@angular/router';
import { MatDialog } from '@angular/material/dialog';
import { DialogStatics, ErrorMessages } from '../../statics/statics';
import { NotificationService } from '../../services/notification/notification.service';
import { ExercisesService } from '../../services/exercises/exercises.service';
import { GetExerciseResponse } from '../../entites/responses/exercise-response';
import { AddExerciseDialogComponent } from '../dialog/add-exercise-dialog/add-exercise-dialog.component';

@Component({
  selector: 'app-exercises',
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
  templateUrl: './exercises.component.html',
  styleUrl: './exercises.component.scss'
})
export class ExercisesComponent implements OnInit {

  readonly dialog = inject(MatDialog);
  private dialogRef: any;
  public exercises: GetExerciseResponse[] = [];
  public filteredExercises: GetExerciseResponse[] = [];
  private subject = new BehaviorSubject(null);

  searchFormControl = new FormControl('', []);

  constructor(
    public exercisesService: ExercisesService,
    private notificationService: NotificationService){}
  
  ngOnInit(): void {
    this.subject
      .pipe(switchMap(() => {
        return this.exercisesService.getExercises('3B161710-B6AD-4F9D-ADF4-6DA489156044');
      }),
      tap((data: GetExerciseResponse[]) => {
        this.exercises = data;
        this.filteredExercises = data;
      })
    ).subscribe();

    this.searchFormControl.valueChanges.subscribe((value) => { 
      this.filteredExercises = this.exercises.filter(exercise => {
        return exercise.name.match(value??'') || exercise.description.match(value??''); 
      })
    })
  }

  public openAddExerciseDialog(){
    this.dialogRef = this.dialog.open(AddExerciseDialogComponent, {});

    this.dialogRef.afterClosed()
    .subscribe((message: string) => {
      if(message === DialogStatics.SUCCESS)
      {
        this.subject.next(null);
      }
    });
  }

  public deleteExerciseDialog(id: string){
    this.exercisesService.deleteExercise(id)
    .subscribe({
      error: (err) => {
        this.notificationService.error(ErrorMessages.DeleteError + err.status);
      },
      next: () => {
        this.notificationService.success(ErrorMessages.DeleteSuccess);
        this.subject.next(null);
      }
    });
  }
}
