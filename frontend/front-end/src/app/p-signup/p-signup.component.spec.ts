import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { PSignupComponent } from './p-signup.component';

describe('PSignupComponent', () => {
  let component: PSignupComponent;
  let fixture: ComponentFixture<PSignupComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ PSignupComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(PSignupComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
