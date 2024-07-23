import { HttpClient } from '@angular/common/http';
import { Component, inject, OnInit } from '@angular/core';
import { NavComponent } from './nav/nav.component';
import { AccountService } from './_services/account.service';
import { RouterOutlet } from '@angular/router';
import { CommonModule } from '@angular/common';
import { HomeComponent } from './home/home.component';

@Component({
  selector: 'app-root',
  

  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css'],
  standalone:true,
  imports: [NavComponent,HomeComponent,RegisterComponent]
})
export class AppComponent implements OnInit {

  private accountService=inject(AccountService);
  title='Dating app';
 



  ngOnInit(): void {

  this.setCurrentUser();

  }
  setCurrentUser()
  {
    const userString=localStorage.getItem('user');
    if (!userString) return;
    const user =JSON.parse(userString);
    this.accountService.currentUser.set(user);
  }

 
}import { ɵAnimationGroupPlayer } from '@angular/animations';
import { NgFor } from '@angular/common';
import { RegisterComponent } from './register/register.component';


