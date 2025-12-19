import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormControl, FormGroup, FormsModule, ReactiveFormsModule, Validators } from '@angular/forms';
import { MatButtonModule } from '@angular/material/button';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatIconModule } from '@angular/material/icon';
import { MatInputModule } from '@angular/material/input';

import { ActivatedRoute } from '@angular/router';
import { switchMap, tap } from 'rxjs';
import { NotificationService } from '../../services/notification/notification.service';
import { ErrorMessages } from '../../statics/statics';
import { ExercisesService } from '../../services/exercises/exercises.service';
import { GetExerciseResponse } from '../../entites/responses/exercise-response';

@Component({
  selector: 'app-exercise',
    standalone: true,
    imports: [
      MatInputModule,
      MatFormFieldModule,
      FormsModule,
      ReactiveFormsModule,
      MatIconModule,
      MatButtonModule],
  templateUrl: './exercise.component.html',
  styleUrl: './exercise.component.scss'
})
export class ExerciseComponent implements OnInit {
  
  public exercise: GetExerciseResponse | undefined;
  public form: FormGroup = new FormGroup({
    name: new FormControl(''),
    videoUrl: new FormControl(''),
    description: new FormControl('')
  });

  constructor(
    public exercisesService: ExercisesService,
    private fb: FormBuilder,
    private route: ActivatedRoute,
    private notificationService: NotificationService
  ){}

  ngOnInit(): void {
    this.route.params.pipe(
      switchMap(params => {
      const exerciseId = params['id'];
      return this.exercisesService.getExercise(exerciseId)
    }),
    tap((data: GetExerciseResponse) => {
      this.exercise = data;

      this.form = this.fb.group({
          id: [this.exercise?.id],
    
          name: [this.exercise?.name, Validators.required],
    
          videoUrl: [this.exercise?.videoUrl, [Validators.required]],
    
          description: [this.exercise?.description, [Validators.required]],
    
      });
    })).subscribe();
  }
  

  save(): void {
    const exerciseId = this.form.get('id')?.value;

    if(this.form.valid && exerciseId){
      let request = {
        name: this.form.get('name')?.value,
        videoUrl: this.form.get('videoUrl')?.value,
        description: this.form.get('description')?.value,
      };

      this.exercisesService.editExercise(exerciseId, request)
        .subscribe({
          error: (err) => {
            this.notificationService.error(ErrorMessages.UpdateError + err.message);
          },
          complete: () => {
            this.notificationService.success(ErrorMessages.UpdateSuccess);
          }
        });
    }
  }

  back(): void {
    window.history.back();
  }
} 
