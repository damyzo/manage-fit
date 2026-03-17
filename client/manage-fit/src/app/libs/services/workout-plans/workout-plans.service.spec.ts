import { TestBed } from '@angular/core/testing';

import { WorkoutPlansService } from './workout-plans.service';

describe('WorkoutPlansService', () => {
  let service: WorkoutPlansService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(WorkoutPlansService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
