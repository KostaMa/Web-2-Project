import { ComponentFixture, TestBed } from '@angular/core/testing';

import { PersonVerificationComponent } from './person-verification.component';

describe('PersonVerificationComponent', () => {
  let component: PersonVerificationComponent;
  let fixture: ComponentFixture<PersonVerificationComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ PersonVerificationComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(PersonVerificationComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
