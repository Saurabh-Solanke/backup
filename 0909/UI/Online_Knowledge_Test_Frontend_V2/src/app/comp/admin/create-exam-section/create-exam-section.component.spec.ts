import { ComponentFixture, TestBed } from '@angular/core/testing';

import { CreateExamSectionComponent } from './create-exam-section.component';

describe('CreateExamSectionComponent', () => {
  let component: CreateExamSectionComponent;
  let fixture: ComponentFixture<CreateExamSectionComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [CreateExamSectionComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(CreateExamSectionComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
