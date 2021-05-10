import { Injectable } from '@angular/core';
import { CanActivate, ActivatedRouteSnapshot, RouterStateSnapshot, Router } from '@angular/router';
import { AuthService } from './auth.service';


@Injectable()
export class AuthGuardService implements CanActivate {

  constructor(private authService: AuthService,
    private router: Router) {  }

  canActivate(route: ActivatedRouteSnapshot, state: RouterStateSnapshot): boolean {
    if (this.authService.isLogin()) {
      return true;
    } else {
      this.router.navigate(['/login']);
      return false;
    }

   return true;
  }

  

  
  
  
}
