import { ComponentFixture, TestBed } from '@angular/core/testing';

import { NutritionsComponent } from './nutritions.component';

describe('NutritionsComponent', () => {
  let component: NutritionsComponent;
  let fixture: ComponentFixture<NutritionsComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [NutritionsComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(NutritionsComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
