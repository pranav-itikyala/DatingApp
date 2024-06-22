import { HttpClient } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { RouterOutlet } from '@angular/router';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-root',
  standalone:true,
  imports:[RouterOutlet,CommonModule],
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent implements OnInit {
  title='Dating app';
  users: any;

  constructor(private http:HttpClient) {}

  ngOnInit(): void {
   this.http.get("https://localhost:5001/api/users").subscribe({
    next: Response => this.users= Response,
    error: error=>console.log(error),
    complete: () => console.log('Request has completed'),

   })

  }
 
}import { ɵAnimationGroupPlayer } from '@angular/animations';
import { NgFor } from '@angular/common';

