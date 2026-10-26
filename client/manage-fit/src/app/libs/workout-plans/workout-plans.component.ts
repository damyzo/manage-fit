import { Component, DestroyRef, inject, OnInit, ViewChild, AfterViewInit } from '@angular/core';
import { FormControl, FormsModule, ReactiveFormsModule } from '@angular/forms';
import { MatButtonModule } from '@angular/material/button';
import { MatTableDataSource, MatTableModule } from '@angular/material/table';
import { MatSort, MatSortModule } from '@angular/material/sort';
import { MatCardModule } from '@angular/material/card';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatIconModule } from '@angular/material/icon';
import { MatInputModule } from '@angular/material/input';
import { RouterLink } from '@angular/router';
import { WorkoutPlansService } from '../services/workout-plans/workout-plans.service';
import { NotificationService } from '../services/notification/notification.service';
import { BehaviorSubject, switchMap, tap } from 'rxjs';
import { GetWorkoutPlanByTrainerIdResponse } from '../entites/responses/workout-plan-response';
// import { NgForOf } from "@angular/common/index";
import { CommonModule } from '@angular/common';
import { takeUntilDestroyed } from '@angular/core/rxjs-interop';
import { MatDialog } from '@angular/material/dialog';
import { WarningDialogData } from '../common/dialogs/warning-dialog-component/interface/warning-dialog-models';
import { WarningDialogComponentComponent } from '../common/dialogs/warning-dialog-component/warning-dialog-component.component';
import { DialogStatics, ErrorMessages } from '../statics/statics';
@Component({
  selector: 'app-workout-plans',
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
    MatSortModule,
    CommonModule
],
  templateUrl: './workout-plans.component.html',
  styleUrl: './workout-plans.component.scss'
})
export class WorkoutPlansComponent implements OnInit, AfterViewInit {
  @ViewChild(MatSort) sort!: MatSort;
  searchFormControl = new FormControl('', []);
  displayedColumns: string[] = ['name', 'exercises', 'clients', 'actions'];
  public workouts: GetWorkoutPlanByTrainerIdResponse[] = [];
  public filteredWorkouts: MatTableDataSource<GetWorkoutPlanByTrainerIdResponse> = new MatTableDataSource<GetWorkoutPlanByTrainerIdResponse>([]);
  private subject = new BehaviorSubject(null);
  private readonly destroyRef = inject(DestroyRef);
  readonly dialog = inject(MatDialog);
  
  private dialogRef: any;
  constructor(
    public workoutPlansService: WorkoutPlansService,
    private notificationService: NotificationService){}

  ngOnInit(): void {
    this.subject
      .pipe(switchMap(() => {
        return this.workoutPlansService.getWorkoutPlansByTrainerId('3B161710-B6AD-4F9D-ADF4-6DA489156044');
      }),
      tap((data: GetWorkoutPlanByTrainerIdResponse[]) => {
        this.workouts = data;
        this.filteredWorkouts.data = data;
      })
    ).subscribe();

    this.searchFormControl.valueChanges.subscribe((value) => { 
      this.filteredWorkouts.data = this.workouts.filter(workout => {
        return workout.name.toLocaleLowerCase().match(value?.toLocaleLowerCase()??''); 
      })
    })
  }

  ngAfterViewInit(): void {
    this.filteredWorkouts.sortingDataAccessor = (workout, column) => {
      switch (column) {
        case 'exercises': return workout.exercises.length;
        case 'clients': return workout.clients.length;
        default: return (workout as any)[column];
      }
    };
    this.filteredWorkouts.sort = this.sort;
  }

  public deleteWorkoutDialog(id: string){
    let warningDialogData: WarningDialogData = {
      title: 'Delete Workout Plan',
      message: 'Are you sure you want to delete this workout plan?',
      yesButtonText: 'Yes',
      noButtonText: 'No'
    };

    this.dialogRef = this.dialog.open(WarningDialogComponentComponent, {data: warningDialogData});

    this.dialogRef.afterClosed()
    .pipe(
      switchMap((message: string) => {
        if(message === DialogStatics.SUCCESS){
          return this.workoutPlansService.deleteWorkoutPlan(id);
        }
        return [];
      }),
      takeUntilDestroyed(this.destroyRef))
    .subscribe({
      error: (err: any) => {
        this.notificationService.error(ErrorMessages.DeleteError + err.status);
      },
      next: (message: any) => {
        if(typeof(message) !== 'string'){
          this.notificationService.success(ErrorMessages.DeleteSuccess);
          this.subject.next(null);
        }
      }
    });
  }
}
