import { ComponentFixture, TestBed } from '@angular/core/testing';
import { provideHttpClient } from '@angular/common/http';
import { MatDialogRef } from '@angular/material/dialog';

import { AddWorkoutPlanDialogComponent } from './add-workout-plan-dialog.component';

describe('AddWorkoutPlanDialogComponent', () => {
  let component: AddWorkoutPlanDialogComponent;
  let fixture: ComponentFixture<AddWorkoutPlanDialogComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [AddWorkoutPlanDialogComponent],
      providers: [
        provideHttpClient(),
        { provide: MatDialogRef, useValue: { close: () => {} } }
      ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(AddWorkoutPlanDialogComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
