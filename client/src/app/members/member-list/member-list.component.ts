import { Component, inject, OnInit } from '@angular/core';
import { MembersService } from '../../_services/members.service';
import { Member } from '../../_models/member';
import { MemberCardComponent } from '../member-card/member-card.component';



@Component({
    selector: 'app-member-list',
    templateUrl: './member-list.component.html',
    styleUrls: ['./member-list.component.css'],
    standalone: true,
    imports: [MemberCardComponent]
})
export class MemberListComponent implements OnInit {
  private memberService=inject(MembersService);
  members:Member[]=[];
  
  



  ngOnInit(): void {
   this.loadMembers();
  }

  loadMembers() {
    
      this.memberService.getMembers().subscribe({
        next: members => this.members=members
         
        }
      )
    }
  

  

  
}