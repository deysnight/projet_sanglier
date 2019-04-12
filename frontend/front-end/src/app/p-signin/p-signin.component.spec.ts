import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { PSigninComponent } from './p-signin.component';

describe('PSigninComponent', () => {
  let component: PSigninComponent;
  let fixture: ComponentFixture<PSigninComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ PSigninComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(PSigninComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
