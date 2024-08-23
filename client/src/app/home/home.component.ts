
import { HttpClient } from '@angular/common/http';
import { Component} from '@angular/core';

import { NgIf } from '@angular/common';
import { RegisterComponent } from "../register/register.component";

@Component({
    selector: 'app-home',
    templateUrl: './home.component.html',
    styleUrls: ['./home.component.css'],
    standalone: true,
    imports: [NgIf, RegisterComponent]
})
export class HomeComponent  {
  registerMode = false;
 
  registerToggle() {
    this.registerMode = !this.registerMode
  }

  cancelRegisterMode(event: boolean) {
    this.registerMode = event;
  }
  
}
