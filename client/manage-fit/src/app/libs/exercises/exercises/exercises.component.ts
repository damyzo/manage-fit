import { Component, inject, OnInit, ViewChild, AfterViewInit, DestroyRef } from '@angular/core';
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
import { MatTableDataSource, MatTableModule } from '@angular/material/table';
import { MatSort, MatSortModule } from '@angular/material/sort';
import { BehaviorSubject, EMPTY, switchMap, tap } from 'rxjs';
import { WarningDialogData } from '../../common/dialogs/warning-dialog-component/interface/warning-dialog-models';
import { WarningDialogComponentComponent } from '../../common/dialogs/warning-dialog-component/warning-dialog-component.component';
import { RouterLink } from '@angular/router';
import { MatDialog } from '@angular/material/dialog';
import { DialogStatics, ErrorMessages } from '../../statics/statics';
import { NotificationService } from '../../services/notification/notification.service';
import { ExercisesService } from '../../services/exercises/exercises.service';
import { GetExerciseResponse } from '../../entites/responses/exercise-response';
import { AddExerciseDialogComponent } from '../dialog/add-exercise-dialog/add-exercise-dialog.component';
import { takeUntilDestroyed } from '@angular/core/rxjs-interop';

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
    MatButtonModule,
    MatTableModule,
    MatSortModule
  ],
  templateUrl: './exercises.component.html',
  styleUrl: './exercises.component.scss'
})
export class ExercisesComponent implements OnInit, AfterViewInit {

  @ViewChild(MatSort) sort!: MatSort;

  readonly dialog = inject(MatDialog);
  private dialogRef: any;
  public exercises: GetExerciseResponse[] = [];
  public filteredExercises: MatTableDataSource<GetExerciseResponse> = new MatTableDataSource<GetExerciseResponse>([]);
  private subject = new BehaviorSubject(null);

  searchFormControl = new FormControl('', []);

  displayedColumns: string[] = ['name', 'description', 'video', 'actions'];

  private readonly destroyRef = inject(DestroyRef);
  constructor(
    public exercisesService: ExercisesService,
    private notificationService: NotificationService){}
  
  ngOnInit(): void {
    this.subject
      .pipe(switchMap(() => {
        return this.exercisesService.getExercises();
      }),
      tap((data: GetExerciseResponse[]) => {
        this.exercises = data;
        this.filteredExercises.data = data;
      })
    ).subscribe();

    this.searchFormControl.valueChanges.subscribe((value) => { 
      this.filteredExercises.data = this.exercises.filter(exercise => {
        return exercise.name.toLocaleLowerCase().match(value?.toLocaleLowerCase()??'')
        || exercise.description.toLocaleLowerCase().match(value?.toLocaleLowerCase()??''); 
      })
    })
  }

  ngAfterViewInit(): void {
    this.filteredExercises.sort = this.sort;
  }

  public openAddExerciseDialog(){
    this.dialogRef = this.dialog.open(AddExerciseDialogComponent, {});

    this.dialogRef.afterClosed()
    .pipe(takeUntilDestroyed(this.destroyRef))
    .subscribe((message: string) => {
      if(message === DialogStatics.SUCCESS)
      {
        this.subject.next(null);
      }
    });
  }

  public deleteExerciseDialog(id: string){
    const warningDialogData: WarningDialogData = {
      title: 'Delete Exercise',
      message: 'Are you sure you want to delete this exercise?',
      yesButtonText: 'Yes',
      noButtonText: 'No'
    };
    this.dialog.open(WarningDialogComponentComponent, {data: warningDialogData})
    .afterClosed()
    .pipe(
      switchMap((message: string) => {
        if(message === DialogStatics.SUCCESS){
              console.log('Deleting exercise with id:', id);

          return this.exercisesService.deleteExercise(id);
        }
        return EMPTY;
      }),
      takeUntilDestroyed(this.destroyRef))
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
