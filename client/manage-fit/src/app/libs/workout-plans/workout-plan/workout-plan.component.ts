import { Component, OnInit } from '@angular/core';
import { CdkDragDrop, DragDropModule, moveItemInArray } from '@angular/cdk/drag-drop';
import { MatIconModule } from '@angular/material/icon';
import { MatButtonModule } from '@angular/material/button';
import { ActivatedRoute } from '@angular/router';
import { switchMap, tap } from 'rxjs';
import { WorkoutPlansService } from '../../services/workout-plans/workout-plans.service';
import { GetWorkoutPlanByTrainerIdResponse } from '../../entites/responses/workout-plan-response';

@Component({
  selector: 'app-workout-plan',
  standalone: true,
  imports: [MatIconModule, MatButtonModule, DragDropModule],
  templateUrl: './workout-plan.component.html',
  styleUrl: './workout-plan.component.scss'
})
export class WorkoutPlanComponent implements OnInit {
  public workoutPlan: GetWorkoutPlanByTrainerIdResponse | undefined;

  constructor(
    private workoutPlansService: WorkoutPlansService,
    private route: ActivatedRoute
  ) {}

  ngOnInit(): void {
    this.route.params.pipe(
      switchMap(params => this.workoutPlansService.getWorkoutPlan(params['id'])),
      tap(data => this.workoutPlan = {
        ...data,
        exercises: [...data.exercises].sort((a, b) => a.order - b.order)
      })
    ).subscribe();
  }

  drop(event: CdkDragDrop<unknown>): void {
    if (!this.workoutPlan) {
      return;
    }
    moveItemInArray(this.workoutPlan.exercises, event.previousIndex, event.currentIndex);
    this.workoutPlan.exercises.forEach((exercise, index) => exercise.order = index + 1);
  }

  back(): void {
    window.history.back();
  }
}
