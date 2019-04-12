import { Component, OnInit, Input } from '@angular/core';
import { FormControl } from '@angular/forms';
import { Observable } from 'rxjs';
import { map, startWith } from 'rxjs/operators';

export interface User {
  name: string;
}

@Component({
  selector: 'app-autocomplete-acca',
  templateUrl: './autocomplete-acca.component.html',
  styleUrls: ['./autocomplete-acca.component.css']
})
export class AutocompleteAccaComponent implements OnInit {

  @Input() accavalue;
  constructor() { }

  myControl = new FormControl();
  options: User[] = [
    {name: 'Mary'},
    {name: 'Shelley'},
    {name: 'Igor'}
  ];
  filteredOptions: Observable<User[]>;

  ngOnInit() {
    this.filteredOptions = this.myControl.valueChanges
      .pipe(
        startWith<string | User>(''),
        map(value => typeof value === 'string' ? value : value.name),
        map(name => name ? this._filter(name) : this.options.slice())
      );
  }

  displayFn(user?: User): string | undefined {
    this.accavalue = user ? user.name : undefined;
    return user ? user.name : undefined;
  }

  private _filter(name: string): User[] {
    const filterValue = name.toLowerCase();

    this.accavalue = filterValue;
    return this.options.filter(option => option.name.toLowerCase().indexOf(filterValue) === 0);
  }

}
