import { Directive, Input, OnInit, TemplateRef, ViewContainerRef, inject } from '@angular/core';
import { take } from 'rxjs';
import { User } from '../_models/user';
import { AccountService } from '../_services/account.service';

@Directive({
    selector: '[appHasRole]',
    standalone: true
})
export class HasRoleDirective implements OnInit {
  private accountService = inject(AccountService);
  private viewContainerRef=inject(ViewContainerRef);
  private templateRef=inject(TemplateRef);

  @Input() appHasRole: string[] = [];
  user = this.accountService.currentUser();



  ngOnInit(): void {
    if (this.accountService?.roles()?.some((r:string) => this.appHasRole.includes(r))) {
      this.viewContainerRef.createEmbeddedView(this.templateRef);
    } else {
      this.viewContainerRef.clear();
    }
  }

}