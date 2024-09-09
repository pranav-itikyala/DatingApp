import { Component, inject,  OnInit,  output } from '@angular/core';
import { AbstractControl, FormBuilder,  FormGroup, FormsModule, ReactiveFormsModule, ValidatorFn, Validators } from '@angular/forms';
import { AccountService } from '../_services/account.service';
import { ToastrService } from 'ngx-toastr';
import { JsonPipe, NgFor, NgIf } from '@angular/common';
import { TextInputComponent } from "../_forms/text-input/text-input.component";
import { DatePickerComponent } from "../_forms/date-picker/date-picker.component";
import { Router } from '@angular/router';

@Component({
  selector: 'app-register',
  standalone: true,
  imports: [ReactiveFormsModule, JsonPipe, NgIf, TextInputComponent, DatePickerComponent,NgFor],
  templateUrl: './register.component.html',
  styleUrl: './register.component.css'
})
export class RegisterComponent implements OnInit {

private accountService=inject(AccountService);
private fb = inject(FormBuilder);
private toastr=inject(ToastrService);
cancelRegister=output<boolean>();
registerForm:FormGroup=new FormGroup({});
maxDate=new Date();
private router=inject(Router);
validationErrors:string[] | undefined;




ngOnInit(): void {
  this.initializeForm();
  this.maxDate.setFullYear(this.maxDate.getFullYear()-18)
}

initializeForm(){
  this.registerForm=this.fb.group({
    gender:['male'],
    username: ['',Validators.required],
    knownAs:['',Validators.required],
    dateOfBirth:['',Validators.required],
    city:['',Validators.required],
    country:['',Validators.required],
    password:['',[Validators.required,Validators.minLength(4)
      ,Validators.maxLength(8)]],
    confirmPassword:['',[Validators.required,this.matchValues('password')]]
  });
  this.registerForm.controls['password'].valueChanges.subscribe({
    next:()=>this.registerForm.controls['confirmPassword'].updateValueAndValidity()
  })
}


matchValues(matchTo:string):ValidatorFn{
  return(control:AbstractControl)=>{
    return control.value===control.parent?.get(matchTo)?.value? null:{isMatching:true}
  }
}



register() {
  const dob = this.GetDateOnly(this.registerForm.controls['dateOfBirth'].value)
  this.registerForm.patchValue({dateOfBirth:dob});
 this.accountService.register(this.registerForm.value).subscribe({
  next: _ => {
    this.router.navigateByUrl('/members')
  },
  error: error => {
    this.validationErrors = error
  } 
    
})
}

cancel()
{
  this.cancelRegister.emit(false);
  console.log('cancelled');
}

private GetDateOnly(dob: string | undefined) {
  if (!dob) return;
  let theDob = new Date(dob);
  return new Date(theDob.setMinutes(theDob.getMinutes()-theDob.getTimezoneOffset())).toISOString().slice(0,10);
}
}
