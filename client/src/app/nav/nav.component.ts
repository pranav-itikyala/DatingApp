import { Component, inject } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { AccountService } from '../_services/account.service';
import { NgIf, TitleCasePipe } from '@angular/common';
import { BsDropdownModule } from 'ngx-bootstrap/dropdown';
import { Router, RouterLink, RouterLinkActive } from '@angular/router';
import { ToastrService } from 'ngx-toastr';

@Component({
  selector: 'app-nav',
  
  imports:[FormsModule,NgIf,BsDropdownModule,RouterLink,RouterLinkActive,TitleCasePipe],
  standalone:true,
  templateUrl: './nav.component.html',
  styleUrls: ['./nav.component.css']
})
export class NavComponent {
  accountService=inject(AccountService);
  router=inject(Router);
  toastr=inject(ToastrService);
  
  model:any={};


login()
{
  this.accountService.login(this.model).subscribe({
    next: _ =>
      {
       void this.router.navigateByUrl('/members');
      },
      error: error=>this.toastr.error(error.error)
  });
  console.log(this.model);
}
logout()
{
 this.accountService.logout();
 this.router.navigateByUrl('/');
}
}
