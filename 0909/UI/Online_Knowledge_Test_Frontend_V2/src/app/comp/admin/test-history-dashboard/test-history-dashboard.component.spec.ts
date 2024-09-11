import { ComponentFixture, TestBed } from '@angular/core/testing';

import { TestHistoryDashboardComponent } from './test-history-dashboard.component';

describe('TestHistoryDashboardComponent', () => {
  let component: TestHistoryDashboardComponent;
  let fixture: ComponentFixture<TestHistoryDashboardComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [TestHistoryDashboardComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(TestHistoryDashboardComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
