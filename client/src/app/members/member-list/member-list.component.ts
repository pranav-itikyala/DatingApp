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
  public memberService=inject(MembersService);
    ngOnInit(): void {
      if(this.memberService.members().length===0) this.loadMembers();

   
  }

  loadMembers() {
    
      this.memberService.getMembers()
    }
  

  

  
}