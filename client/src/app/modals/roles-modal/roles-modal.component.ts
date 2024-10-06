import { NgFor } from '@angular/common';
import { Component, inject } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { BsModalRef } from 'ngx-bootstrap/modal';

@Component({
  selector: 'app-roles-modal',
  standalone: true,
  imports: [FormsModule, NgFor],
  templateUrl: './roles-modal.component.html',
  styleUrl: './roles-modal.component.css'
})
export class RolesModalComponent {
  bsModalRef=inject(BsModalRef);
  username = '';
  title='';
  list: string[]=[];
  availableRoles: string[] = [];
  selectedRoles: string[] = [];
  rolesUpdated=false;

  

  updateChecked(checkedValue: string) {
   if(this.selectedRoles.includes(checkedValue))
    this.selectedRoles=this.selectedRoles.filter(r=>r !==checkedValue)

    else
    this.selectedRoles.push(checkedValue);
    

  }

  onSelectRoles()
  {
    this.rolesUpdated=true;
    this.bsModalRef.hide();
    
  }


}
