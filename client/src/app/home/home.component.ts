
import { HttpClient } from '@angular/common/http';
import { Component, inject, OnInit } from '@angular/core';

import { NgIf } from '@angular/common';
import { RegisterComponent } from "../register/register.component";

@Component({
    selector: 'app-home',
    templateUrl: './home.component.html',
    styleUrls: ['./home.component.css'],
    standalone: true,
    imports: [NgIf, RegisterComponent]
})
export class HomeComponent implements OnInit {
  registerMode = false;
  users: any;
  http = inject(HttpClient);

  constructor() {}

  ngOnInit(): void {
    this.getUsers()
  }

  registerToggle() {
    this.registerMode = !this.registerMode
  }

  cancelRegisterMode(event: boolean) {
    this.registerMode = event;
  }
  getUsers()
  {
    this.http.get("http://localhost:5001/api/users").subscribe({
      next: Response => this.users= Response,
      error: error=>console.log(error),
      complete: () => console.log('Request has completed')
  
     })
  }

}
