import { Injectable } from '@angular/core';
import { ActivatedRouteSnapshot, CanActivate, Router, RouterStateSnapshot } from '@angular/router';
import { AuthService } from '../services/auth.service';

@Injectable({
  providedIn: 'root'
})
export class AuthGuard implements CanActivate {
  constructor(private authService: AuthService, private router: Router) { }

  canActivate(next: ActivatedRouteSnapshot, state: RouterStateSnapshot): boolean {
    const isLoggedIn = this.authService.isLoggedIn();
    const routeRoles = next.firstChild.data.roles as string[];

    if (routeRoles && isLoggedIn) {
      const isPermitted = this.authService.checkPermissions(routeRoles);

      if (isPermitted) {
        return true;
      }

      this.router.navigate(['']);
    }

    if (isLoggedIn) {
      return true;
    }

    this.router.navigate(['/auth'], { queryParams: { returnTo: state.url } });

    return false;
  }
}
