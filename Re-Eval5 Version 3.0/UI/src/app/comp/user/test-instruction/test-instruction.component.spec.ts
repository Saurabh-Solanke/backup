import { ComponentFixture, TestBed } from '@angular/core/testing';

import { TestInstructionComponent } from './test-instruction.component';

describe('TestInstructionComponent', () => {
  let component: TestInstructionComponent;
  let fixture: ComponentFixture<TestInstructionComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [TestInstructionComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(TestInstructionComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
