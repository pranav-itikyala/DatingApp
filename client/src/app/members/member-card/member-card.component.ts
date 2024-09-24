import { Component, computed, inject, input, Input, ViewEncapsulation } from '@angular/core';
import { ToastrService } from 'ngx-toastr';
import { RouterLink } from '@angular/router';
import { NgIf, AsyncPipe } from '@angular/common';
import { Member } from '../../_models/member';
import { LikesService } from '../../_services/likes.service';

@Component({
    selector: 'app-member-card',
    templateUrl: './member-card.component.html',
    styleUrls: ['./member-card.component.css'],
    standalone: true,
    imports: [NgIf, RouterLink, AsyncPipe],
    encapsulation:ViewEncapsulation.None
})
export class MemberCardComponent {
  private likeService=inject(LikesService);
  member=input.required<Member>();
  hasLiked=computed(()=>this.likeService.likeIds().includes(this.member().id));
  toggleLike()
  {
    this.likeService.toggleLike(this.member().id).subscribe({
      next:()=>{
        if(this.hasLiked())
        {
          this.likeService.likeIds.update(ids=> ids.filter(x=>x!== this.member().id))
        }
        else{
          this.likeService.likeIds.update(ids=>[...ids,this.member().id])
        }
      }
    })
  }
  }

