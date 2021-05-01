import { Directive, Input, OnInit, TemplateRef, ViewContainerRef } from '@angular/core';
import { AuthService } from '../services/auth.service';

@Directive({
  selector: '[appRequiredRoles]'
})
export class RequiredRolesDirective implements OnInit {
  @Input() appRequiredRoles: string[];

  constructor(private viewContainerRef: ViewContainerRef, private templateRef: TemplateRef<any>, private authService: AuthService) { }

  ngOnInit() {
    const isPermitted = this.authService.checkPermissions(this.appRequiredRoles);

    if (isPermitted) {
      this.viewContainerRef.createEmbeddedView(this.templateRef);
    } else {
      this.viewContainerRef.clear();
    }
  }
}
