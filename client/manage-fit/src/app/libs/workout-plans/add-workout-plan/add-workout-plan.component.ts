import { Component, DestroyRef, OnInit, inject } from '@angular/core';
import { MatSelectModule } from '@angular/material/select';
import { MatIconModule } from '@angular/material/icon';
import { ExercisesService } from '../../services/exercises/exercises.service';
import { ClientsService } from '../../services/clients/clients.service';
import { GetClientResponse } from '../../entites/responses/client-response';
import { GetExerciseResponse } from '../../entites/responses/exercise-response';
import { AddWorkoutPlanExerciseRequest } from '../../entites/requests/workout-plan-request';
import { FormControl, FormGroup, FormsModule, ReactiveFormsModule, Validators } from '@angular/forms';
import { MatButtonModule } from '@angular/material/button';
import { Router } from '@angular/router';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';
import { takeUntilDestroyed } from '@angular/core/rxjs-interop';
import { WorkoutPlansService } from '../../services/workout-plans/workout-plans.service';
import { NotificationService } from '../../services/notification/notification.service';
import { AddWorkoutPlanRequest } from '../../entites/requests/workout-plan-request';
import { ErrorMessages } from '../../statics/statics';

interface PlanExercise {
  key: number;
  exercise: GetExerciseResponse;
  set: number;
  repetitions: number;
  time: number;
}

@Component({
  selector: 'app-add-workout-plan',
  standalone: true,
  imports: [
    MatFormFieldModule,
    MatInputModule,
    FormsModule,
    ReactiveFormsModule,
    MatButtonModule,
    MatSelectModule,
    MatIconModule
  ],
  templateUrl: './add-workout-plan.component.html',
  styleUrl: './add-workout-plan.component.scss'
})
export class AddWorkoutPlanComponent implements OnInit {
  private readonly destroyRef = inject(DestroyRef);
  private readonly router = inject(Router);

  public form: FormGroup = new FormGroup({
    name: new FormControl('', Validators.required)
  });

  public exercises: GetExerciseResponse[] = [];
  public exerciseSearch = '';

  get filteredExercises(): GetExerciseResponse[] {
    const term = this.exerciseSearch.trim().toLowerCase();
    return term
      ? this.exercises.filter(e => e.name.toLowerCase().includes(term))
      : this.exercises;
  }
  public planExercises: PlanExercise[] = [];
  private nextKey = 0;
  public showNewExercise = false;
  public clients: GetClientResponse[] = [];
  public selectedClientIds: string[] = [];
  public clientSearch = '';

  // Options are hidden rather than removed so selections survive filtering.
  isClientVisible(client: GetClientResponse): boolean {
    const term = this.clientSearch.trim().toLowerCase();
    return !term
      || client.name.toLowerCase().includes(term)
      || client.email.toLowerCase().includes(term);
  }


  public newExerciseForm: FormGroup = new FormGroup({
    name: new FormControl('', Validators.required),
    description: new FormControl(''),
    videoUrl: new FormControl('')
  });

  constructor(
    private workoutPlansService: WorkoutPlansService,
    private exercisesService: ExercisesService,
    private clientsService: ClientsService,
    private notificationService: NotificationService) {}

  ngOnInit(): void {
    this.clientsService.getClients()
      .pipe(takeUntilDestroyed(this.destroyRef))
      .subscribe({
        next: (clients) => this.clients = clients,
        error: (err) => this.notificationService.error(ErrorMessages.InsertError + err.status)
      });
    this.exercisesService.getExercises()
      .pipe(takeUntilDestroyed(this.destroyRef))
      .subscribe({
        next: (exercises) => this.exercises = exercises,
        error: (err) => this.notificationService.error(ErrorMessages.InsertError + err.status)
      });
  }

  countOf(exerciseId: string): number {
    return this.planExercises.filter(p => p.exercise.id === exerciseId).length;
  }

  // The same exercise can be added multiple times; each entry has its own settings.
  addExercise(exercise: GetExerciseResponse | null): void {
    if (!exercise) {
      return;
    }
    this.planExercises = [
      ...this.planExercises,
      { key: this.nextKey++, exercise, set: 0, repetitions: 0, time: 0 }
    ];
  }

  move(index: number, direction: -1 | 1): void {
    const target = index + direction;
    if (target < 0 || target >= this.planExercises.length) {
      return;
    }
    const list = [...this.planExercises];
    [list[index], list[target]] = [list[target], list[index]];
    this.planExercises = list;
  }

  remove(index: number): void {
    this.planExercises = this.planExercises.filter((_, i) => i !== index);
  }

  toggleNewExercise(): void {
    this.showNewExercise = !this.showNewExercise;
  }

  createExercise(): void {
    if (this.newExerciseForm.invalid) {
      return;
    }

    const value = this.newExerciseForm.value;
    this.exercisesService.addExercise({
      name: value.name,
      description: value.description ?? '',
      videoUrl: value.videoUrl ?? ''
    })
      .pipe(takeUntilDestroyed(this.destroyRef))
      .subscribe({
        next: (created) => {
          this.exercises = [...this.exercises, created];
          this.addExercise(created);
          this.newExerciseForm.reset({ name: '', description: '', videoUrl: '' });
          this.showNewExercise = false;
          this.notificationService.success(ErrorMessages.InsertSuccess);
        },
        error: (err) => this.notificationService.error(ErrorMessages.InsertError + err.status)
      });
  }

  save(): void {
    if (this.form.invalid) {
      this.form.markAllAsTouched();
      this.notificationService.warning('Please enter a name for the workout plan.');
      return;
    }

    const request: AddWorkoutPlanRequest = {
      name: this.form.get('name')?.value,
      exercises: this.planExercises.map((p, i): AddWorkoutPlanExerciseRequest => ({
        id: p.exercise.id,
        name: p.exercise.name,
        description: p.exercise.description,
        videoUrl: p.exercise.videoUrl,
        set: Number(p.set) || 0,
        repetitions: Number(p.repetitions) || 0,
        time: Number(p.time) || 0,
        order: i + 1
      })),
      clientUids: this.selectedClientIds
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
          this.router.navigate(['/workouts']);
        }
      });
  }

  close(): void {
    this.router.navigate(['/workouts']);
  }
}
