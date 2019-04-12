import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { AutocompleteAccaComponent } from './autocomplete-acca.component';

describe('AutocompleteAccaComponent', () => {
  let component: AutocompleteAccaComponent;
  let fixture: ComponentFixture<AutocompleteAccaComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ AutocompleteAccaComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(AutocompleteAccaComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
