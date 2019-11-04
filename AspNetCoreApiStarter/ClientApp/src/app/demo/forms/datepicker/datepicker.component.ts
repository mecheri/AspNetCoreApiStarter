import { Component } from '@angular/core';
import { DateAdapter } from '@angular/material';
import { FormControl } from '@angular/forms';
import { MatDatepickerInputEvent } from '@angular/material/datepicker';

@Component({
  selector: 'app-datepicker',
  templateUrl: './datepicker.component.html',
  styleUrls: ['./datepicker.component.scss']
})
export class DatepickerComponent {
  // this is for the start date
  startDate = new Date(1990, 0, 1);

  minDate = new Date(2000, 0, 1);
  maxDate = new Date(2020, 0, 1);

  // Datepicker selected value
  date = new FormControl(new Date());
  serializedDate = new FormControl(new Date().toISOString());

  // Datepicker input and change event

  events: string[] = [];

  addEvent(type: string, event: MatDatepickerInputEvent<Date>) {
    this.events.push(`${type}: ${event.value}`);
  }

  myFilter = (d: Date): boolean => {
    const day = d.getDay();
    // Prevent Saturday and Sunday from being selected.
    return day !== 0 && day !== 6;
    // tslint:disable-next-line:semicolon
  };

  constructor(private adapter: DateAdapter<any>) {}

  french() {
    this.adapter.setLocale('fr');
  }
}
