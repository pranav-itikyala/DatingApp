import { Component, inject, OnInit } from '@angular/core';
import { AdminService } from '../../_services/admin.service';
import { User } from '../../_models/user';
import { RolesModalComponent } from '../../modals/roles-modal/roles-modal.component';
import { BsModalRef, BsModalService, ModalOptions } from 'ngx-bootstrap/modal';

@Component({
  selector: 'app-user-management',
  standalone: true,
  imports: [],
  templateUrl: './user-management.component.html',
  styleUrls: ['./user-management.component.css']
})
export class UserManagementComponent implements OnInit{

private adminServices =inject(AdminService);
users : User[]=[];

constructor(private adminService: AdminService, private modalService: BsModalService) { }


bsModalRef:BsModalRef<RolesModalComponent>= new BsModalRef<RolesModalComponent>();

ngOnInit(): void{
  this.getUsersWithRoles();
}



openRolesModal(user:User)
{
  const initialState:ModalOptions={
    class:'modal-lg',
    initialState:{
      title:'User roles',
      username:user.username,
      selectedRoles:[...user.roles],
      availableRoles:['Admin','Moderator','Member'],
      users:this.users,
      rolesUpdated:false,
      list:['Admin','Moderator','Member']
    }
  }
  this.bsModalRef=this.modalService.show(RolesModalComponent,initialState);
  this.bsModalRef.onHide?.subscribe (
    {
      next:() => {
        if(this.bsModalRef.content && this.bsModalRef.content.rolesUpdated)
        {
          const selectedRoles=this.bsModalRef.content.selectedRoles;
          this.adminServices.updateUserRoles(user.username,selectedRoles).subscribe({
            next:roles=>user.roles=roles
          })
          
        }

      }
    }
  )
}




getUsersWithRoles()
{
  this.adminServices.getUsersWithRoles().subscribe({
    next:users=>this.users=users
  })
}









}
