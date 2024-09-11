import { ComponentFixture, TestBed } from '@angular/core/testing';

import { PassportOfficeListComponent } from './passport-office-list.component';

describe('PassportOfficeListComponent', () => {
  let component: PassportOfficeListComponent;
  let fixture: ComponentFixture<PassportOfficeListComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [PassportOfficeListComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(PassportOfficeListComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
