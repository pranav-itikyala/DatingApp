import { Component, input, Input, ViewEncapsulation } from '@angular/core';
import { ToastrService } from 'ngx-toastr';
import { RouterLink } from '@angular/router';
import { NgIf, AsyncPipe } from '@angular/common';
import { Member } from '../../_models/member';

@Component({
    selector: 'app-member-card',
    templateUrl: './member-card.component.html',
    styleUrls: ['./member-card.component.css'],
    standalone: true,
    imports: [NgIf, RouterLink, AsyncPipe],
    encapsulation:ViewEncapsulation.None
})
export class MemberCardComponent {
  member=input.required<Member>();
  }

