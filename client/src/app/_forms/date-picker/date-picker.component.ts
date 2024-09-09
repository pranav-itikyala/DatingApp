import { Component, input, Input, Self } from '@angular/core';
import { ControlValueAccessor, FormControl, NgControl, FormsModule, ReactiveFormsModule } from '@angular/forms';
import { BsDatepickerConfig, BsDatepickerModule } from 'ngx-bootstrap/datepicker';
import { NgIf } from '@angular/common';

@Component({
    selector: 'app-date-picker',
    templateUrl: './date-picker.component.html',
    styleUrls: ['./date-picker.component.css'],
    standalone: true,
    imports: [BsDatepickerModule, FormsModule, ReactiveFormsModule, NgIf]
})
export class DatePickerComponent implements ControlValueAccessor {
  label = input<string>('');
  maxDate=input<Date>();
  
  bsConfig: Partial<BsDatepickerConfig> | undefined;

  constructor(@Self() public ngControl: NgControl) {
    this.ngControl.valueAccessor = this;
    this.bsConfig = {
      containerClass: 'theme-red',
      dateInputFormat: 'DD MMMM YYYY',
    }
  }
  
  writeValue(obj: any): void {
  }

  registerOnChange(fn: any): void {
  }

  registerOnTouched(fn: any): void {
  }

  get control(): FormControl {
    return this.ngControl.control as FormControl;
  }
}