import { Injectable } from "@angular/core";
import { ActivatedRouteSnapshot, CanActivate, Router, RouterStateSnapshot, UrlTree } from "@angular/router";
import { Observable } from "rxjs";

import { JwtHelperService } from '@auth0/angular-jwt';

@Injectable({
    providedIn: 'root'
})

export class DelivererGuard implements CanActivate {
    constructor(private router: Router, private helper: JwtHelperService) {}

    canActivate(route: ActivatedRouteSnapshot, state: RouterStateSnapshot): boolean | UrlTree | Observable<boolean | UrlTree> | Promise<boolean | UrlTree> {
        let token = localStorage.getItem('token');
        if(token != null) {
            let role = localStorage.getItem('role');
            let activate = localStorage.getItem('activate');
            console.log(role + ' ' + activate);

            if(!this.helper.isTokenExpired(token) && role === 'Deliverer' && activate === 'Active') {
                return true;
            }
            this.router.navigateByUrl('/home');
            return false;
        }
        this.router.navigateByUrl('');
        return false;
    }

}