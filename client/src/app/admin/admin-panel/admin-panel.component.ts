import { Component } from '@angular/core';
import { HasRoleDirective } from '../../_directives/has-role.directive';
import { TabsModule } from 'ngx-bootstrap/tabs';
import { PhotoManagementComponent } from "../photo-management/photo-management.component";
import { UserManagementComponent } from '../user-management/user-management.component';

@Component({
    selector: 'app-admin-panel',
    templateUrl: './admin-panel.component.html',
    styleUrls: ['./admin-panel.component.css'],
    standalone: true,
    imports: [TabsModule, HasRoleDirective, UserManagementComponent, PhotoManagementComponent]
})
export class AdminPanelComponent {

}